using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Users.Entities
{
    public class User : AuditableEntity
    {
        public Guid UserID => Id; // Id es heredado de AuditableEntity
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public bool IsActive { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public DateTime? HiresDate { get; set; } // Agregado para registrar la fecha de contratación del usuario


        /// <summary>
        /// una vez que se ha creado un usuario, no se puede modificar su Id, lo que garantiza la integridad de los datos y la consistencia en el sistema.
        /// </summary>
        private User() { }


        /// <summary>
        /// permite crear un nuevo usuario con los datos necesarios, asignando un Id único y estableciendo el estado activo por defecto.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <param name="fullName"></param>
        public User(string email, string username, string passwordHash, string fullName, DateTime? hireDate = null)
        {
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            Username = username;
            IsActive = true;
            TwoFactorEnabled = false;
            HiresDate = hireDate.Value == null ? DateTime.UtcNow: hireDate;
        }

        /// <summary>
        /// permite actualizar el correo electrónico del usuario, lo que es útil para mantener la información de contacto actualizada. 
        /// </summary>
        /// <param name="newEmail"></param>
        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }

        /// <summary>
        /// permite desactivar el usuario
        /// 
        /// </summary>
        public void DisableAccount()
        {
            IsActive = false;
        }

    }
}
