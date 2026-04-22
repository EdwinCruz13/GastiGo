using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Transaction?>> GetTIncomeAndExpenseByUserAndYearAsync(Guid UserID, Int32 yearId);

        Task<IEnumerable<Transaction?>> GetTSavingsByUserAndYearAsync(Guid UserID, Guid accountId, Int32 yearId);
        Task<IEnumerable<Transaction?>> GetTInvestmentByUserAndYearAsync(Guid UserID, Guid accountId, Int32 yearId);
    }
}
