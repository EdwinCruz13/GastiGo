using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.DTOs
{
    /// <summary>
    /// dto que representa el resultado de una autenticación exitosa, incluyendo el token de acceso (AccessToken) y el token de actualización (RefreshToken)
    /// </summary>
    public class AuthResult
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
