using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public static class NatureDependencyInjection
    {
        public static IServiceCollection AddNaturesInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<INatureRepository, NatureRepository>();

            return services;
        }
    }
}
