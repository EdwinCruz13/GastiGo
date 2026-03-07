using Application.Auth.DTOs;
using Application.Common;
using Application.Features.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace GastiGo.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// registrar un nuevo usuario en el sistema
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario creado correctamente",
                Data = null
            });
        }

        /// <summary>
        /// se autentica un usuario existente y se genera un token de acceso para el usuario autenticado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Login exitoso",
                Data = token
            });
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> Verify2FA(VerifyTwoFactorRequest request)
        {
            var result = await _authService.VerifyTwoFactorAsync(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "2FA verificado",
                Data = result
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var result = await _authService.RefreshAsync(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Token renovado",
                Data = result
            });
        }

       
    }
}
