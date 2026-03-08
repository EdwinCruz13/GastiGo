using Application.Common;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/transactionypes")]
    public class TransactionTypeController : ControllerBase
    {
        private readonly AccountTypeService _accountTypeService;

        /// <summary>
        /// inyecta el servicio a consumir
        /// </summary>
        /// <param name="accountTypeService"></param>
        public TransactionTypeController(AccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }


        /// <summary>
        /// endpoint que retorna la lista de tipo de transactions
        /// si no encuentra devuelve un mensaje de error
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _accountTypeService.GetAllAccountTypesAsync();
            if (list == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existe tipos de transacciones registradas",
                    Data = null,
                    Errors = new List<string> { "No se encontraron tipos de transacciones en la base de datos." }
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
        /// endpoint que retorna un tipo de transaction por su id, 
        /// si no se encuentra devuelve un mensaje de error,
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _accountTypeService.GetAccountTypeByIdAsync(id);
            if (item == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existe tipo de transacción con el id proporcionado",
                    Data = null,
                    Errors = new List<string> { $"No se encontró un tipo de transacción con el id {0}." }
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
