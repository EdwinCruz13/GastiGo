using Application.Features.Finances.Interfaces;
using Application.Features.Public.Interfaces;
using Infrastructure.Repositories.Finances;
using Infrastructure.Repositories.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.DI
{
    public static class ExchangeRateDependencyInjection
    {
        public static IServiceCollection AddExchangeRateInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

            return services;
        }
    }
}
