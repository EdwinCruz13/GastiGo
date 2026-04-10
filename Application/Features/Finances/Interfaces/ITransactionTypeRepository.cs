using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<IEnumerable<TransactionType?>> GetAllTransactionTypesAsync();
        Task<TransactionType?> GetTransactionTypeByIdAsync(Guid id);
        Task IncrementCurrentValueAsync(Guid id);

       

    }
}
