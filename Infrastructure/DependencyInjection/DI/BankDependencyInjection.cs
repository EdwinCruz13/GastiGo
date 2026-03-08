using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection.DI
{
    public static class BankDependencyInjection
    {
        public static IServiceCollection AddBanksInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IBankRepository, BankRepository>();

            return services;
        }
    }
}
