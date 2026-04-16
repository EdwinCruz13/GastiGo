
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Infrastructure.DependencyInjection.DI;

namespace Infrastructure.DependencyInjection
{
    /// <summary>
    /// permite adjuntar todas las dependencias de la capa de infraestructura a la coleccion de servicios de la aplicacion
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default")));

            services.AddUOWInfrastructure();
            services.AddAuthInfrastructure();
            services.AddUsersInfrastructure();
            services.AddCategoriesInfrastructure();
            services.AddCategoriesParamInfrastructure();
            services.AddCurrenciesInfrastructure();
            services.AddBanksInfrastructure();
            services.AddNaturesInfrastructure();
            services.AddAccountTypeInfrastructure();
            services.AddAccountInfrastructure();
            services.AddTransactionTypeInfrastructure();
            services.AddTransactionInfrastructure();
            services.AddIncomeTaxInfrastructure();

            return services;
        }
    }
}
