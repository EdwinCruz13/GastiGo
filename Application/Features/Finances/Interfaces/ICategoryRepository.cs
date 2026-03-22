using Domain.Features.Finances.Entities;


namespace Application.Features.Finances.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<List<Category>> GetCategoryByUserIdAsync(Guid userId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> HasChildrenAsync(Guid parentId);
        Task SaveChangesAsync();
    }
}
