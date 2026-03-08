using Application.Common;
using Application.Features.Finances.DTOs;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        /// <summary>
        /// injecta el servicio de cuentas para manejar las operaciones relacionadas con las cuentas financieras.
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// crear una nueva categoría de finanzas
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(AccountDTO response)
        {
            await _accountService.CreateAccountAsync(response);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cuenta creada correctamente",
                Data = null
            });
        }

        /// <summary>
        /// obtener una categoría por su ID, si no se encuentra la categoría, se devuelve un mensaje de error con un código de estado 404 (Not Found).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var list = await _accountService.GetAccountByIDAsync(id);
            if (list == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "",
                    Data = null,
                    Errors = new List<string> { $"No se encontró una cuenta con el ID {id}." }
                });
            }


            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = list
            });
        }

        /// <summary>
        /// obtener las categorías de un usuario por su ID, si no se encuentran categorías para el usuario especificado, se devuelve un mensaje de error con un código de estado 404 (Not Found).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByUserID([FromQuery] Guid userId)
        {
            var item = await _accountService.GetAllAccountsByUserIDAsync(userId);
            if (item == null || !item.Any())
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No se encontraron cuentas para el usuario especificado",
                    Data = null,
                    Errors = new List<string> { $"No se encontraron cuentas para el usuario con ID {userId}." }
                });
            }
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "",
                Data = item
            });
        }
    }
}
