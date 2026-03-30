using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Finances
{
    public class CategoryParamService : ICategoryParamRepository
    {
        readonly private AppDbContext _context;

        public CategoryParamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryParams?>> GetAllCategoryParamsAsync()
        {
            return await _context.CategoryParams.Include(g => g.Category).ToListAsync();
        }

        public Task<CategoryParams?> GetCategoryParamByIdAsync(Guid id)
        {
            return _context.CategoryParams.FirstOrDefaultAsync(cp => cp.Id == id);
        }

        public Task SaveChangesAsync()
        {
           _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
