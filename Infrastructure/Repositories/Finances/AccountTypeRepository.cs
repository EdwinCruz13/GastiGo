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
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor de la clase AccountTypeRepository, recibe una instancia de AppDbContext para interactuar con la base de datos.
        /// </summary>
        /// <param name="context"></param>
        public AccountTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountType?> GetAccountTypeByIdAsync(Guid id)
        {
            return await _context.AccountTypes.FirstOrDefaultAsync(at => at.Id == id);
        }

        public async Task<IEnumerable<AccountType?>> GetAllAccountTypesAsync()
        {
            return await _context.AccountTypes.ToListAsync();
        }
    }
}
