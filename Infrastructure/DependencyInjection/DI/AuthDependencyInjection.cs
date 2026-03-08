using Application.Features.Auth.Interfaces;
using Infrastructure.Repositories.Auths;
using Infrastructure.Services.Auths;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection.DI
{
    /// <summary>
    /// anade la dependicas de los servicios de auth a la coleccion de servicios de la aplicacion
    /// </summary>
    public static class AuthDependencyInjection
    {
        public static IServiceCollection AddAuthInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITwoFactorRepository, TwoFactorRepository>();

            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
