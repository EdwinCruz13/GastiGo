using Microsoft.EntityFrameworkCore;

using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Application.Features.Finances.DTOs;



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

        public async Task AddAsync(Bank bankCreateDTO)
        {
            await _context.Banks.AddAsync(bankCreateDTO);
        }

        public Task UpdateAsync(Bank bankUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bank?>> GetAllBanksAsync()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<Bank?> GetBankByIdAsync(Guid id)
        {
            return await _context.Banks.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
