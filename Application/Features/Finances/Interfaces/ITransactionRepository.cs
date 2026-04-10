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
        Task<IEnumerable<Transaction?>> GetTransactionsByUserIDAsync(Guid UserID);
        Task<IEnumerable<Transaction?>> GetTransactionsByUserIDAndTimeAsync(Guid UserID, Guid CuentaId, DateTime FechaInicio, DateTime FechaFin);

        Task<Transaction?> GetTransactionByIDAsync(Guid id);
        Task SaveChangesAsync();
    }
}
