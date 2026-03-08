using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Finances
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// injecta el contexto de la base de datos para poder realizar las operaciones necesarias
        /// </summary>
        /// <param name="context"></param>
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _context.Accounts.Where(a => a.Id == id).Include(u => u.User).Include(t => t.AccountType).Include(b => b.Bank).Include(c => c.Currecy).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account?>> GetAllAccountsByUserIDAsync(Guid UserID)
        {
            return await _context.Accounts.Where(a => a.UserID == UserID).Include(u => u.User).Include(t => t.AccountType).Include(b => b.Bank).Include(c => c.Currecy).ToListAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == account.AccountID);
            _context.Entry(account).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
