using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.
builder.Services.AddapplicationServices().
    AddInfrastructureServices(builder.Configuration).AddApiServices();
   
var app = builder.Build();

//COnfigure the HTTP request pipeline.

app.Run();
