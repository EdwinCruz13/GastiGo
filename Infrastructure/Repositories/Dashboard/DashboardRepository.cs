using Application.Features.Dashboard.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Repositories.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction?>> GetTIncomeAndExpenseByUserAndYearAsync(Guid UserID, int yearId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == UserID && t.TransactionDate.Year == yearId && t.TransferGroupID == null && t.Category.Nature.Abbre == "E" || t.Category.Nature.Abbre == "I")
                .Include(t => t.TransactionType)
                .Include(c => c.Category).ThenInclude(b => b.Nature)
                .Include(a => a.Account).ThenInclude(d => d.Bank)
                .Include(a => a.Account).ThenInclude(ac => ac.AccountType)
                
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction?>> GetTSavingsByUserAndYearAsync(Guid UserID, Guid AccountId, int yearId)
        {
            return await _context.Transactions
                 .Where(t => t.UserId == UserID && t.TransactionDate.Year == yearId && t.TransferGroupID == null && t.AccountId == AccountId)
                 .Include(t => t.TransactionType)
                 .Include(c => c.Category).ThenInclude(b => b.Nature)
                 .Include(a => a.Account).ThenInclude(d => d.Bank)
                 .Include(a => a.Account).ThenInclude(ac => ac.AccountType)

                 .ToListAsync();
        }

        public async Task<IEnumerable<Transaction?>> GetTInvestmentByUserAndYearAsync(Guid UserID, Guid AccountId, int yearId)
        {
            return await _context.Transactions
                 .Where(t => t.UserId == UserID && t.TransactionDate.Year == yearId && t.TransferGroupID == null && t.AccountId == AccountId)
                 .Include(t => t.TransactionType)
                 .Include(c => c.Category).ThenInclude(b => b.Nature)
                 .Include(a => a.Account).ThenInclude(d => d.Bank)
                 .Include(a => a.Account).ThenInclude(ac => ac.AccountType)

                 .ToListAsync();
        }
    }
}
