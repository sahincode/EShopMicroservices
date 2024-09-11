using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Extension;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.
builder.Services.AddApplicationServices().
    AddInfrastructureServices(builder.Configuration).AddApiServices();
;
   
var app = builder.Build();

//COnfigure the HTTP request pipeline.
app.UserApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitialDatabaseAsync();
}

app.Run();
