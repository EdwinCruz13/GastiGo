
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;

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

            services.AddAuthInfrastructure();
            services.AddUsersInfrastructure();
            services.AddCategoriesInfrastructure();
            services.AddCurrenciesInfrastructure();
            services.AddBanksInfrastructure();
            services.AddNaturesInfrastructure();
            services.AddAccountTypeInfrastructure();

            return services;
        }
    }
}
