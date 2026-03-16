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
            return await _context.Categories.Include(n => n.Nature).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Categories.Where(x => x.UserId == userId).Include(n => n.Nature).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Category category)
        {
             _context.Categories.Update(category);
            return Task.CompletedTask;
        }
    }
}
