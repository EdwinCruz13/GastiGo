using Application.Common;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{

    [ApiController]
    [Route("api/finance/accountypes")]
    public class AccountTypeController : ControllerBase
    {
        private readonly AccountTypeService _accountTypeService;

        /// <summary>
        /// constructor del controlador de tipos de cuentas, el cual se encarga de manejar las operaciones relacionadas con los tipos de cuentas
        /// </summary>
        /// <param name="accountTypeService"></param>
        public AccountTypeController(AccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        /// <summary>
        /// metodo para obtener todos los tipos de cuentas, si no se encuentran tipos de cuentas devuelve un mensaje de error,
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
                    Message = "No existe tipos de cuentas registradas",
                    Data = null,
                    Errors = new List<string> { "No se encontraron tipos de cuentas en la base de datos." }
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
        /// metodo para obtener un tipo de cuenta por su id, si no se encuentra devuelve un mensaje de error,
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _accountTypeService.GetAccountTypeByIdAsync(id);
            if (item == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Tipo de cuenta no encontrada",
                    Data = null,
                    Errors = new List<string> { $"No se encontró el tipo de cuentas con el ID: {id}" }
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
