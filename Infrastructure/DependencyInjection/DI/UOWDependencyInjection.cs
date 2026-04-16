using Application.Features.Finances.Interfaces;
using Application.Features.UnitOfWork;
using Infrastructure.Repositories.Finances;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.DI
{
    public static class UOWDependencyInjection
    {
        public static IServiceCollection AddUOWInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
