using Application.Common;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        /// <summary>
        /// Controlador para manejar las operaciones relacionadas con las monedas
        /// </summary>
        /// <param name="categoryService"></param>
        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }


        /// <summary>
        /// obtiene todas las monedas, si no se encuentran devuelve un mensaje de error,
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync();

            if (currencies == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existe monedas registradas",
                    Data = null,
                    Errors = new List<string> { "No se encontraron monedas en la base de datos." }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = currencies
            });
        }

        /// <summary>
        /// busca una moneda por su id, si no se encuentra devuelve un mensaje de error, 
        /// si se encuentra devuelve la moneda encontrada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Moneda no encontrada",
                    Data = null,
                    Errors = new List<string> { $"No se encontró una moneda con el ID: {id}" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = currency
            });

        }
    }
}
