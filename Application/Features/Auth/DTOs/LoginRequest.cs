using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.DTOs
{
    /// <summary>
    /// representa la solicitud de inicio de sesión, conteniendo las propiedades necesarias para que un usuario pueda autenticarse, como el correo electrónico y la contraseña.
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
