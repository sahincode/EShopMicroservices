
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Extension
{
    public static class DatabaseExtensions
    {
        public  static async Task InitialDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
             context.Database.MigrateAsync().GetAwaiter().GetResult();

        }
    }
}
