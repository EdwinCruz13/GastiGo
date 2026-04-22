using Domain.Features.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Interfaces
{
    public interface IExchangeRateRepository
    {
        public Task<ExchangeRate?> GetCurrentExchangeRateAsync(Guid fromCurrency, Guid toCurrency);
        public Task<List<ExchangeRate>> GetAllExchangeRatesAsync(Guid fromCurrency, Guid toCurrency);
        public Task AddExchangeRateAsync(ExchangeRate exchangeRate);
        public Task SaveChangesAsync();
    }
}
