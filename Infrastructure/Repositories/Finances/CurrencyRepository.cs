using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Finances
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly AppDbContext _context;

        /// <summary>
        /// constructor para inicializar el contexto de la base de datos
        /// </summary>
        /// <param name="dbContext"></param>
        public CurrencyRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Currency?>> GetAllCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency?> GetCurrencyByIdAsync(Guid id)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
