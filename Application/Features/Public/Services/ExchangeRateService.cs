using Application.Features.Finances.DTOs;
using Application.Features.Public.DTOs;
using Application.Features.Public.Interfaces;
using Application.Features.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Services
{
    public class ExchangeRateService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository, IUnitOfWork unitofWork)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// retorna el tipo de cambio entre dos monedas, se utiliza para convertir una cantidad de una moneda a otra, se devuelve un objeto con la información del tipo de cambio, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExchangeRateDTO> GetCurrentExchageRate(Guid fromCurrency, Guid toCurrency)
        {
            var exchangeRate = await _exchangeRateRepository.GetCurrentExchangeRateAsync(fromCurrency, toCurrency);
            if (exchangeRate == null)
                throw new Exception("No se encontró la tasa de cambio para las monedas especificadas.");


            return new ExchangeRateDTO
            {
                CurrencyFrom = new CurrencyDTO { CurrencyId = exchangeRate.CurrencyFromId, Name = exchangeRate.CurrencyFrom.Name, Code = exchangeRate.CurrencyFrom.Code, Symbol = exchangeRate.CurrencyFrom.Symbol },
                CurrencyTo = new CurrencyDTO { CurrencyId = exchangeRate.CurrencyToId, Name = exchangeRate.CurrencyTo.Name, Code = exchangeRate.CurrencyTo.Code, Symbol = exchangeRate.CurrencyTo.Symbol },
                Value = exchangeRate.Value,
                Date = exchangeRate.Date
            };
        }

        /// <summary>
        /// retorna todas las tasas de cambio entre dos monedas, se utiliza para mostrar la evolución del tipo de cambio entre dos monedas, se devuelve una lista de objetos con la información del tipo de cambio, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ExchangeRateDTO>> GetAllExchangeRatesAsync(Guid fromCurrency, Guid toCurrency)
        {
            var exchangeRates = await _exchangeRateRepository.GetAllExchangeRatesAsync(fromCurrency, toCurrency);
            if (exchangeRates == null || exchangeRates.Count == 0)
                throw new Exception("No se encontraron tasas de cambio para las monedas especificadas.");

            return exchangeRates.Select(exchangeRate => new ExchangeRateDTO
            {
                CurrencyFrom = new CurrencyDTO { CurrencyId = exchangeRate.CurrencyFromId, Name = exchangeRate.CurrencyFrom.Name, Code = exchangeRate.CurrencyFrom.Code, Symbol = exchangeRate.CurrencyFrom.Symbol },
                CurrencyTo = new CurrencyDTO { CurrencyId = exchangeRate.CurrencyToId, Name = exchangeRate.CurrencyTo.Name, Code = exchangeRate.CurrencyTo.Code, Symbol = exchangeRate.CurrencyTo.Symbol },
                Value = exchangeRate.Value,
                Date = exchangeRate.Date
            }).ToList();
        }

        /// <summary>
        /// inserta una nueva tasa de cambio entre dos monedas, se utiliza para guardar el valor del tipo de cambio en una fecha determinada, se recibe un objeto con la información del tipo de cambio, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="exchangeRateDTO"></param>
        /// <returns></returns>
        public async Task AddExchangeRateAsync(ExchangeRateDTO exchangeRateDTO)
        {
            try
            {
                var exchangeRate = new Domain.Features.Public.ExchangeRate(exchangeRateDTO.Date, exchangeRateDTO.Value, exchangeRateDTO.CurrencyFrom.CurrencyId, exchangeRateDTO.CurrencyTo.CurrencyId);
                await _exchangeRateRepository.AddExchangeRateAsync(exchangeRate);
                await _exchangeRateRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la tasa de cambio: " + ex.Message);
            }
        }

        /// <summary>
        /// inserta una lista de nuevas tasas de cambio entre dos monedas, se utiliza para guardar el valor del tipo de cambio en una fecha determinada, se recibe un objeto con la información del tipo de cambio, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="exchangeRateDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddExchangeRateBulkAsync(int year, decimal value, Guid CurrencyFrom, Guid CurrencyTo)
        {


            DateTime FechaInicia = new DateTime(year, 1, 1);
            DateTime FechaFin = new DateTime(year, 12, 31);

            FechaInicia = DateTime.SpecifyKind(FechaInicia, DateTimeKind.Utc).ToUniversalTime();
            FechaFin = DateTime.SpecifyKind(FechaFin, DateTimeKind.Utc).ToUniversalTime();

            try
            {


                // Iniciar la transacción
                await _unitOfWork.BeginTransactionAsync();

                // Insertar tasas de cambio para cada día del año
                while (FechaInicia <= FechaFin)
                {
                    // Crear una nueva tasa de cambio para la fecha actual
                    var exchangeRate = new Domain.Features.Public.ExchangeRate(FechaInicia, value, CurrencyFrom, CurrencyTo);
                    await _exchangeRateRepository.AddExchangeRateAsync(exchangeRate);

                    // Avanzar al siguiente día
                    FechaInicia = FechaInicia.AddDays(1);
                }



                // GUARDAR
                await _unitOfWork.SaveChangesAsync();
                //COMMIT FINAL
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // ROLLBACK EN CASO DE ERROR
                await _unitOfWork.RollbackAsync();
                throw new Exception("Error al agregar la tasa de cambio: " + ex.Message);
            }
        }




    }
}
