using Application.Common;
using Application.Features.Finances.DTOs;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        /// <summary>
        /// injecta el servicio de cuentas para manejar las operaciones relacionadas con las cuentas financieras.
        /// </summary>
        /// <param name="transactionService"></param>
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// crear una nueva transaccion de finanzas
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(TransactionDTO response)
        {
            await _transactionService.AddTransactionAsync(response);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Transaccion creada correctamente",
                Data = null
            });
        }

        /// <summary>
        /// busca una transaccion por id, si no se encuentra la transaccion, se devuelve un mensaje de error con un código de estado 404 (Not Found).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var list = await _transactionService.GetTransactionByIDAsync(id);
            if (list == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "",
                    Data = null,
                    Errors = new List<string> { $"No se encontró ninguna transaccion con el ID {id}." }
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
        /// busca las transsacciones por usuario, si no se encuentra ninguna transaccion, se devuelve un mensaje de error con un código de estado 404 (Not Found).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByUserID([FromQuery] Guid userId)
        {
            var item = await _transactionService.GetAllTransactionsByUserIDAsync(userId);
            if (item == null || !item.Any())
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No se encontraron transacciones para el usuario especificado",
                    Data = null,
                    Errors = new List<string> { $"No se encontraron transacciones para el usuario con ID {userId}." }
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
