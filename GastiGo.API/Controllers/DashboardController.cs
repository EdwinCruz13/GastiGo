using Application.Common;
using Application.Features.Dashboard.Services;
using Application.Features.Finances.DTOs;
using Application.Features.Finances.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// obtiene las transacciones de un usuario por año, se categoriza por mes y se devuelve un objeto con la información de las transacciones, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("getIncomeAndExpensesByYear")]
        public async Task<IActionResult> DashboardByYear([FromQuery] Guid userId, [FromQuery] int year)
        {
            var list = await _dashboardService.GetTIncomeAndExpenseByUserAndYearAsync(userId, year);

            if (list == null)
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
                Data = list
            });
        }


        /// <summary>
        /// obtiene el ahorro total de un usuario por año, se categoriza por mes y se devuelve un objeto con la información de las transacciones, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="year"></param>
        /// <returns></returns>

        [HttpGet("GetSavingsByUserAndYear")]
        public async Task<IActionResult> GetSavingsByUserAndYear([FromQuery] Guid userId, [FromQuery] int year)
        {
            var list = await _dashboardService.GetTSavingsByUserAndYearAsync(userId, year);

            if (list == null)
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
                Data = list
            });
        }

        [HttpGet("GetInvestmentByUserAndYear")]
        public async Task<IActionResult> GetInvestmentByUserAndYear([FromQuery] Guid userId, [FromQuery] int year)
        {
            var list = await _dashboardService.GetTInvestmentByUserAndYearAsync(userId, year);

            if (list == null)
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
                Data = list
            });
        }


    }
}
