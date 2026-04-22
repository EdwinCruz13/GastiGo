using Application.Common;
using Application.Features.Public.Services;
using Domain.Features.Users.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/public/exchangerate")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ExchangeRateService _exchangeRateService;
        public ExchangeRateController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRate([FromQuery] Guid FromCurrencyId, [FromQuery] Guid ToCurrencyId)
        {
            var exchangeRate = await _exchangeRateService.GetCurrentExchageRate(FromCurrencyId, ToCurrencyId);

            if (exchangeRate == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No se encontró la tasa de cambio para las monedas especificadas",
                    Data = null,
                    Errors = new List<string> { $"No se encontro tasa de cambio actual" }
                });
            }


            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Tipo de cambio obtenido correctamente",
                Data = exchangeRate
            });
        }

        [HttpGet("GetExchangeRateHistory")]
        public async Task<IActionResult> GetExchangeRateHistory([FromQuery] Guid FromCurrencyId, [FromQuery] Guid ToCurrencyId)
        {
            var exchangeRates = await _exchangeRateService.GetAllExchangeRatesAsync(FromCurrencyId, ToCurrencyId);


            if (exchangeRates == null)
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No se encontraron tasas de cambio para las monedas especificadas",
                    Data = null,
                    Errors = new List<string> { $"No se encontraron tasas de cambio para las monedas especificadas" }
                });


            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = exchangeRates,
            });
        }

        [HttpPost("BulkExchageRate")]
        public async Task<IActionResult> BulkExchageRate([FromQuery] int year, [FromQuery] Guid CurrencyFrom, [FromQuery] Guid CurrencyTo, [FromQuery] decimal value)
        {
            try
            {
                await _exchangeRateService.AddExchangeRateBulkAsync(year, value, CurrencyFrom, CurrencyTo);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Tasa de cambio insertada para el año " + year.ToString(),
                    Data = null
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
