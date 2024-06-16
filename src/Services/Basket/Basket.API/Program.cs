



using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
// add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketReposiitory>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
   
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//configure the http request pipeline
app.MapCarter();
app.UseExceptionHandler(options =>
{

});

app.Run();
