using Microsoft.Extensions.DependencyInjection;


namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public  static IServiceCollection AddapplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
