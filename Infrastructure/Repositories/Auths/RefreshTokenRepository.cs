using Microsoft.EntityFrameworkCore;
using Application.Features.Auth.Interfaces;
using Domain.Features.Auth.Entities;
using Infrastructure.Persistence;



namespace Infrastructure.Repositories.Auths
{
    /// <summary>
    /// permite manejar la persistencia de los tokens de actualización (refresh tokens) en la base de datos. 
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
            => await _context.RefreshTokens.AddAsync(token);

        public async Task<RefreshToken?> GetByTokenAsync(string token)
            => await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
