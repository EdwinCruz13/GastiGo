using Domain.Features.Finances.Entities;

namespace Application.Features.Finances.Interfaces
{
    public interface ICategoryParamRepository
    {
            Task<IEnumerable<CategoryParams?>> GetAllCategoryParamsAsync();
            Task<CategoryParams?> GetCategoryParamByIdAsync(Guid id);
            Task SaveChangesAsync();
    }
}
