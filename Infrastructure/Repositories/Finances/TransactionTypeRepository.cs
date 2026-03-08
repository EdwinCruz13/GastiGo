using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Finances
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {

        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// inyecta el dbcontext
        /// </summary>
        /// <param name="appDbContext"></param>
        public TransactionTypeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<TransactionType?>> GetAllTransactionTypesAsync()
        {
            return await _appDbContext.TransactionTypes.ToListAsync();
        }

        public async Task<TransactionType?> GetTransactionTypeByIdAsync(Guid id)
        {
            return await _appDbContext.TransactionTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task IncrementCurrentValueAsync(Guid id)
        {
            await _appDbContext.TransactionTypes.Where(t => t.Id == id).ExecuteUpdateAsync(s => s.SetProperty(t => t.CurrentValue, t => t.CurrentValue + 1));
        }
    }
}
