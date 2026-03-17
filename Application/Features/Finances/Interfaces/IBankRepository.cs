using Application.Features.Finances.DTOs;
using Domain.Features.Finances.Entities;


namespace Application.Features.Finances.Interfaces
{
    public interface IBankRepository
    {
        Task AddAsync(Bank bankCreateDTO);
        Task UpdateAsync(Bank bankUpdateDTO);
        Task<IEnumerable<Bank?>> GetAllBanksAsync();
        Task<Bank?> GetBankByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
