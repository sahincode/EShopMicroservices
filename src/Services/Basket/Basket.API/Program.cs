



using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
// add services to the container
//Application Services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
//Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketReposiitory>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
    opts.InstanceName= builder.Configuration.GetSection("Redis:InstanceName").Value;
    opts.ConfigurationOptions= ConfigurationOptions.Parse(builder.Configuration.GetSection("Redis:Configuration").Value!);
    opts.ConfigurationOptions.ConnectTimeout = 5000;
    opts.ConfigurationOptions.SyncTimeout = 5000;
    opts.ConfigurationOptions.AbortOnConnectFail = false;


});
//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts =>
{
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});
//Async Communication Services 
builder.Services.AddMessageBroker(builder.Configuration);


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().
    AddNpgSql(builder.Configuration.GetConnectionString("Database")!).
    AddRedis(builder.Configuration.GetConnectionString("Redis")!);
var app = builder.Build();

//configure the http request pipeline
app.MapCarter();
app.UseExceptionHandler(options =>
{

});
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse

    });
app.Run();
