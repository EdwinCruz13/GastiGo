using Application.Features.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {

            //anadir servicios de la capa de aplicacion
            services.AddScoped<AuthService>();

            //retornar servicios
            return services;
        }
    }
}
