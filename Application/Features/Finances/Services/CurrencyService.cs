using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
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
                CurrencyId = currency.Id,
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

            //verificar que tenga monedas registradas, si no tiene devuelve null, si tiene devuelve la lista de monedas encontradas
            if (currencies != null || currencies.Any())
            {
                //ordenar por Symbol, primero las monedas con el símbolo "C$", luego las monedas con el símbolo "$",
                //y finalmente las demás monedas, ordenadas alfabéticamente por su símbolo
                currencies = currencies
                            .OrderBy(m => m.Symbol == "C$" ? 0 :
                                          m.Symbol == "$" ? 1 : 2)
                            .ThenBy(m => m)
                            .ToList();
            }



            return currencies.Select(c => c == null ? null : new CurrencyDTO
            {
                CurrencyId = c.Id,
                Name = c.Name,
                Code = c.Code,
                Symbol = c.Symbol
            });
        }
    }
}
