using Microsoft.EntityFrameworkCore;

using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;



namespace Infrastructure.Repositories.Finances
{
    public class BankRepository : IBankRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// constructor para inicializar el contexto de la base de datos
        /// </summary>
        /// <param name="context"></param>
        public BankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bank?>> GetAllBanksAsync()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<Bank?> GetBankByIdAsync(Guid id)
        {
            return await _context.Banks.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
