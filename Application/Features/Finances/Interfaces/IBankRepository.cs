using Application.Features.Finances.DTOs;
using Domain.Features.Finances.Entities;


namespace Application.Features.Finances.Interfaces
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank?>> GetAllBanksAsync();
        Task<Bank?> GetBankByIdAsync(Guid id);
    }
}
