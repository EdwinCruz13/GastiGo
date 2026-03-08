using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account?>> GetAllAccountsByUserIDAsync(Guid UserID);
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task SaveChangesAsync();
    }
}
