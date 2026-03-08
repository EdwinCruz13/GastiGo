using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Finances.DTOs;
using Application.Features.Finances.Services;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/finance/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        /// <summary>
        /// Controlador para manejar las operaciones relacionadas con las categorías
        /// Este controlador se encarga de recibir las solicitudes HTTP relacionadas 
        /// con las categorías y delegar la lógica de negocio al servicio correspondiente.
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /// <summary>
        /// crear una nueva categoría de finanzas
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO response)
        {
            await _categoryService.CreateCategoryAsync(response);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Categoria creada correctamente",
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
            var list = await _categoryService.GetCategoryByIdAsync(id);
            if (list == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Categoria no encontrada",
                    Data = null,
                    Errors = new List<string> { $"No se encontró una categoría con el ID {id}." }
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
            var item = await _categoryService.GetCategoriesByUserIdAsync(userId);
            if (item == null || !item.Any())
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No se encontraron categorías para el usuario especificado",
                    Data = null,
                    Errors = new List<string> { $"No se encontraron categorías para el usuario con ID {userId}." }
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
