
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public  static async Task InitialDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
             context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);

        }
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomerAsync();
        }
        private static async Task SendCustomerAsync(ApplicationDbContext context)
        {
            if(!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);
                await context.SaveChangesAsync();
            }

        }
    }
}
