using Application.Features.Finances.Interfaces;
using Infrastructure.Repositories.Finances;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.DI
{
    
    public static class TransactionTypeDependencyInjection
    {
        public static IServiceCollection AddTransactionTypeInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

            return services;
        }
    }
}
