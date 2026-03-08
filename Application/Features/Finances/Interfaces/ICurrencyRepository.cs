using Domain.Features.Finances.Entities;


namespace Application.Features.Finances.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency?>> GetAllCurrenciesAsync();
        Task<Currency?> GetCurrencyByIdAsync(Guid id);
    }
}
