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
    
     public static class TransaTransactionDependencyInjectionctionTypeDependencyInjection
    {
        public static IServiceCollection AddTransactionInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionService>();

            return services;
        }
    }
}
