using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Services
{
    public class CurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }


        /// <summary>
        /// obtiene una moneda por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurrencyDTO?> GetCurrencyByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("El ID de la moneda no puede ser vacío.");

            //buscar en la bd
            var currency = await _currencyRepository.GetCurrencyByIdAsync(id);

            //si no se encuentra la moneda, devuelve null, si se encuentra devuelve la moneda encontrada
            return currency == null ? null : new CurrencyDTO
            {
                CurrencyID = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Symbol = currency.Symbol
            };


        }

        /// <summary>
        /// obtiene todas las monedas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyDTO?>> GetAllCurrenciesAsync()
        {
           var currencies = await _currencyRepository.GetAllCurrenciesAsync();
           return currencies.Select(c => c == null ? null : new CurrencyDTO
           {
               CurrencyID = c.Id,
               Name = c.Name,
               Code = c.Code,
               Symbol = c.Symbol
           });
        }
    }
}
