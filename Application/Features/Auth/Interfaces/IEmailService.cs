using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Interfaces
{
    /// <summary>
    /// interface para el servicio de correo electrónico, que define un método para enviar un código de verificación para la autenticación de dos factores a una dirección de correo electrónico específica.
    /// </summary>
    public interface IEmailService
    {

        /// <summary>
        /// permite enviar un correo electrónico con un código de verificación para la autenticación de dos factores, 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task SendTwoFactorCodeAsync(string email, string code);
    }
}
