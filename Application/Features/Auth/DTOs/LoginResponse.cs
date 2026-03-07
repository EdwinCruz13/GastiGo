using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.DTOs
{
    /// <summary>
    /// permite representar la respuesta de un intento de inicio de sesión, indicando si se requiere autenticación de dos factores para completar el proceso de inicio de sesión. 
    /// </summary>
    public class LoginResponse
    {
        public bool RequiresTwoFactor { get; set; }
        public Guid? TwoFactorId { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
