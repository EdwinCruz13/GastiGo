using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Domain.Features.Users.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Finances
{
    public class TransactionService : ITransactionRepository
    {
        private readonly AppDbContext _context;


        /// <summary>
        /// inyecta el contexto de la base de datos para poder realizar operaciones CRUD en la entidad Transaction
        /// </summary>
        /// <param name="context"></param>
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction request)
        {
            await _context.Transactions.AddAsync(request);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByUserIDAsync(Guid UserID)
        {
            return await _context.Transactions.Where(u => u.UserId == UserID)
                .Include(u => u.User)
                .Include(t => t.TransactionType)
                .Include(c => c.Category).Include(n => n.Category.Nature)
                .Include(a => a.Account).Include(a => a.Account.Bank).Include(a => a.Account.Currency).Include(a => a.Account.AccountType).Include(a => a.Account.User)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIDAsync(Guid id)
        {
            return await _context.Transactions.Where(u => u.Id == id)
                .Include(u => u.User)
                .Include(t => t.TransactionType)
                .Include(c => c.Category).Include(n => n.Category.Nature)
                .Include(a => a.Account).Include(a => a.Account.Bank).Include(a => a.Account.Currency).Include(a => a.Account.AccountType).Include(a => a.Account.User)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
