using Application.Common;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{

    [ApiController]
    [Route("api/finance/natures")]
    public class NatureController : ControllerBase
    {
        private readonly NatureService _natureService;

        /// <summary>
        /// instancia el controlador de naturaleza, el cual se encarga de manejar las operaciones relacionadas con las naturalezas
        /// </summary>
        /// <param name="natureService"></param>
        public NatureController(NatureService natureService)
        {
            _natureService = natureService;
        }

        /// <summary>
        /// metodo para obtener todas las naturalezas, si no se encuentran naturalezas devuelve un mensaje de error,
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _natureService.GetAllNaturesAsync();

            if (list == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No existe tipos de naturalezas registradas",
                    Data = null,
                    Errors = new List<string> { "No se encontraron tipos de naturalezas en la base de datos." }
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
        /// metodo para obtener una naturaleza por su id, si no se encuentra devuelve un mensaje de error,
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _natureService.GetNatureByIdAsync(id);
            if (item == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Tipo de naturaleza no encontrada",
                    Data = null,
                    Errors = new List<string> { $"No se encontró el Tipo de naturaleza con el ID: {id}" }
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
