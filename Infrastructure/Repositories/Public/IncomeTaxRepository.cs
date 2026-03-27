using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Features.Public.Interfaces;
using Domain.Features.Public.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Public
{
    public class IncomeTaxRepository : IIncomeTaxRepository
    {

        private readonly AppDbContext _context;

        public IncomeTaxRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IncomeTax?>> GetAllIncomeTax()
        {
            return await _context.IncomeTaxes.ToListAsync();
        }

        public async Task<IncomeTax?> GetIncomeTaxByIdAsync(int id)
        {
            return await _context.IncomeTaxes.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
