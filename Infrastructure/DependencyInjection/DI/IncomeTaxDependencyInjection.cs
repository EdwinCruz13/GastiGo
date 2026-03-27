using Application.Features.Public.Interfaces;
using Infrastructure.Repositories.Public;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection.DI
{
    public static class IncomeTaxDependencyInjection
    {
        public static IServiceCollection AddIncomeTaxInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IIncomeTaxRepository, IncomeTaxRepository>();

            return services;
        }
    }
}
