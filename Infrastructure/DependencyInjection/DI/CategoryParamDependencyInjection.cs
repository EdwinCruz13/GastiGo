using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.DependencyInjection.DI
{
    /// <summary>
    /// injecta las dependencias de los servicios relacionados con las categorías 
    /// a la colección de servicios de la aplicación,
    /// </summary>
    public static class CategoryParamDependencyInjection
    {
        public static IServiceCollection AddCategoriesParamInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ICategoryParamRepository, CategoryParamRepository>();

            return services;
        }
    }
}
