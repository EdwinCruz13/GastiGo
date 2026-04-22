using Application.Features.Public.Interfaces;
using Domain.Features.Public;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Public
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRateRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// obtiene el tipo de cambio entre dos monedas, se utiliza para convertir
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <returns></returns>
        public async Task<ExchangeRate?> GetCurrentExchangeRateAsync(Guid fromCurrency, Guid toCurrency)
        {

            var Today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Today = DateTime.SpecifyKind(Today, DateTimeKind.Utc).ToUniversalTime();


            return await _context.ExchangeRate
                .Where(er => er.CurrencyFromId == fromCurrency && er.CurrencyToId == toCurrency && er.Date == Today)
                .Include(cf => cf.CurrencyFrom)
                .Include(ct => ct.CurrencyTo)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Lista de tipos de cambio entre dos monedas, se utiliza para mostrar el historial de tipos de cambio entre dos monedas, se ordena por fecha descendente
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <returns></returns>
        public async Task<List<ExchangeRate>> GetAllExchangeRatesAsync(Guid fromCurrency, Guid toCurrency)
        {
            return await _context.ExchangeRate.Where(er => er.CurrencyFromId == fromCurrency && er.CurrencyToId == toCurrency).ToListAsync();
        }

        /// <summary>
        /// crea un nuevo tipo de cambio entre dos monedas, se utiliza para guardar el valor del tipo de cambio en una fecha determinada, se valida que el valor del tipo de cambio sea mayor que cero y que las monedas no sean iguales
        /// </summary>
        /// <param name="exchangeRate"></param>
        /// <returns></returns>
        public async Task AddExchangeRateAsync(ExchangeRate exchangeRate)
        {
            _context.ExchangeRate.Add(exchangeRate);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
