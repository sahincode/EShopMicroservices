using BuildingBlocks.Behaviours;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);
var assembly=typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
   
}).UseLightweightSessions();
var app = builder.Build();

app.MapCarter();

app.Run();
