using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction request);
        Task<IEnumerable<Transaction?>> GetAllTransactionsByUserIDAsync(Guid UserID);
        Task<Transaction?> GetByIDAsync(Guid id);
        Task SaveChangesAsync();
    }
}
