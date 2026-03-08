using Domain.Features.Finances.Entities;

namespace Application.Features.Finances.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task<IEnumerable<AccountType?>> GetAllAccountTypesAsync();
        Task<AccountType?> GetAccountTypeByIdAsync(Guid id);
    }
}
