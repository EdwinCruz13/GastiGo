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
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Categories.Where(x => x.UserID == userId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            await _context.Categories.FindAsync(category.CategoryID);
            _context.Entry(category).State = EntityState.Modified;
        }
    }
}
