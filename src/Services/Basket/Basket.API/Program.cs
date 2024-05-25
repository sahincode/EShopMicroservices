

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

var app = builder.Build();

//configure the http request pipeline
app.MapCarter();

app.Run();
