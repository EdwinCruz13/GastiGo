using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection.DI
{
    public static class CurrencyDependencyInjection
    {
        public static IServiceCollection AddCurrenciesInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            return services;
        }
    }
}
