using Application.Common;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/banks")]
    public class BankController : ControllerBase
    {

        private readonly BankService _bankService;

        public BankController(BankService bankService)
        {
            _bankService = bankService;
        }

        /// <summary>
        /// obtiene todas las bnacos, si no se encuentran devuelve un mensaje de error,
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var banks = await _bankService.GetAllBanksAsync();

            if (banks == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existe bancos registrados",
                    Data = null,
                    Errors = new List<string> { "No se encontraron bancos en la base de datos." }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = banks
            });
        }

        /// <summary>
        /// busca un banco por su id, si no se encuentra devuelve un mensaje de error, 
        /// si se encuentra devuelve el banco encontrada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bank = await _bankService.GetBankByIdAsync(id);
            if (bank == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Banco no encontrado",
                    Data = null,
                    Errors = new List<string> { $"No se encontró un banco con el ID: {id}" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = bank
            });

        }
    }
}
