using Domain.Features.Auth.Entities;

namespace Application.Features.Auth.Interfaces
{
    /// <summary>
    /// interfaz que define las operaciones necesarias para manejar los tokens de actualización (refresh tokens) en el sistema de autenticación. 
    /// </summary>
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
