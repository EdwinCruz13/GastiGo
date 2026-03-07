using Application.Features.Users.Interfaces;
using Infrastructure.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    /// <summary>
    /// anade la dependicas de los servicios de users a la coleccion de servicios de la aplicacion
    /// </summary>
    public static class UsersDependencyInjection
    {
        public static IServiceCollection AddUsersInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
