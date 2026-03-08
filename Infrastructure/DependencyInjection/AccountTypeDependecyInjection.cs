using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection
{
    
    public static class AccountTypeDependecyInjection
    {
        public static IServiceCollection AddAccountTypeInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();

            return services;
        }
    }
}
