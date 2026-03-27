using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection.DI
{
    public static class NatureDependencyInjection
    {
        public static IServiceCollection AddNaturesInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<INatureRepository, NatureRepository>();

            return services;
        }
    }
}
