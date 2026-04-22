using Application.Features.Dashboard.Interfaces;
using Infrastructure.Repositories.Dashboard;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection.DI
{
    public static class DashboardDependencyInjection
    {
        public static IServiceCollection AddDashboardInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            return services;
        }
    }
}
