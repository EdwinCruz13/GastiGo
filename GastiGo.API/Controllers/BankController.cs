using Application.Common;
using Application.Features.Finances.DTOs;
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
        /// crea un nuevo banco
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(BankDTO response)
        {
            await _bankService.CreateBankAsync(response);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Banco creado correctamente",
                Data = null
            });
        }

        /// <summary>
        /// actualiza el banco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, BankDTO response)
        {
            await _bankService.UpdateBankAsync(id, response);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Banco actualizado correctamente",
                Data = null
            });
        }

        /// <summary>
        /// obtiene todas las bnacos, si no se encuentran devuelve un mensaje de error,
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var banks = await _bankService.GetAllBanksAsync();

            // Si no se encuentran bancos, devuelve un mensaje de error,
            // pero no es un error del servidor, sino una respuesta válida indicando que no hay datos disponibles.
            if (banks == null || !banks.Any())
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existen bancos registrados",
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
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bank = await _bankService.GetBankByIdAsync(id);
            if (bank == null)
            {
                return Ok(new ApiResponse<object>
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
