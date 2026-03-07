using Microsoft.EntityFrameworkCore;
using Domain.Features.Auth.Entities;
using Infrastructure.Persistence;
using Application.Features.Auth.Interfaces;

namespace Infrastructure.Repositories.Auths
{
    public class TwoFactorRepository : ITwoFactorRepository
    {
        private readonly AppDbContext _context;

        public TwoFactorRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// permite recuperar un código de autenticación de dos factores específico utilizando su identificador único (twoFactorId).
        /// </summary>
        /// <param name="twoFactorId"></param>
        /// <returns></returns>
        public async Task<TwoFactorCode?> GetByIdAsync(Guid twoFactorId)
        {
            return await _context.TwoFactorCodes
                .FirstOrDefaultAsync(x => x.Id == twoFactorId);
        }

        public async Task AddCodeAsync(TwoFactorCode code)
            => await _context.TwoFactorCodes.AddAsync(code);


        /// <summary>
        /// valida un código de autenticación de dos factores específico utilizando su identificador único (twoFactorId) y el código proporcionado por el usuario. 
        /// </summary>
        /// <param name="twoFactorId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<TwoFactorCode?> ValidateCodeAsync(Guid twoFactorId, string code)
            => await _context.TwoFactorCodes
                .FirstOrDefaultAsync(x =>
                    x.Id == twoFactorId &&
                    x.Code == code &&
                    x.Status.TwoFactorStatusID == 1 &&
                    x.ExpiresAt > DateTime.UtcNow);



        /// <summary>
        /// obtener todos los códigos de autenticación de dos factores activos (no usados y no expirados) asociados a un usuario específico, identificando al usuario mediante su identificador único (userId).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<TwoFactorCode>> GetActiveByUserAsync(Guid userId)
        {
            return await _context.TwoFactorCodes
                .Where(x => x.UserID == userId &&
                            x.Status.TwoFactorStatusID == 1)
                .ToListAsync();
        }

        /// <summary>
        /// invalida todos los códigos activos (no usados y no expirados) para un usuario específico, marcándolos como usados para evitar que puedan ser reutilizados.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task InvalidateActiveCodesAsync(Guid userId)
        {
            var activeCodes = await GetActiveByUserAsync(userId);

            foreach (var code in activeCodes)
            {
                code.MarkAsReplaced();
            }
        }

        /// <summary>
        /// guardar los cambios realizados en el contexto de la base de datos, 
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();

        
    }
}
