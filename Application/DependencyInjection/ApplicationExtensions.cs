using Application.Features.Auth.Services;
using Application.Features.Finances.Interfaces;
using Application.Features.Finances.Services;
using Application.Features.Public.Services;
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
            services.AddScoped<CategoryService>();
            services.AddScoped<CategoryParamService>();
            services.AddScoped<CurrencyService>();
            services.AddScoped<BankService>();
            services.AddScoped<NatureService>();
            services.AddScoped<AccountTypeService>();
            services.AddScoped<AccountService>();
            services.AddScoped<TransactionTypeService>();
            services.AddScoped<TransactionService>();
            services.AddScoped<IncomeTaxService>();
            
           

            //retornar servicios
            return services;
        }
    }
}
