using Domain.Features.Auth.Entities;

namespace Application.Features.Auth.Interfaces
{
    /// <summary>
    /// interface para manejar la lógica relacionada con la autenticación de dos factores
    /// </summary>
    public interface ITwoFactorRepository
    {
        Task AddCodeAsync(TwoFactorCode code);
        Task<TwoFactorCode?> ValidateCodeAsync(Guid twoFactorId, string code);
        Task<TwoFactorCode?> GetByIdAsync(Guid twoFactorId);
        Task InvalidateActiveCodesAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
