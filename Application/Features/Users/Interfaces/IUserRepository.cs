using Domain.Features.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.Users.Interfaces
{
    /// <summary>
    /// define los métodos para interactuar con la entidad User, como obtener un usuario por correo electrónico o ID, agregar un nuevo usuario, actualizar un usuario existente y guardar los cambios en la base de datos.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// obtiene un usuario por su correo electrónico
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string email);
        /// <summary>
        /// obtiene un usuario por su nombre de usuario
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User?> GetByUsernameAsync(string username);
        /// <summary>
        /// obtiene un usuario por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id);
        /// <summary>
        /// metodo para guardar nuevo usuario en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(User user);
        /// <summary>
        /// metodo para actualizar un usuario existente en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateAsync(User user);
        /// <summary>
        /// metodo para guardar los cambios realizados en la base de datos
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
