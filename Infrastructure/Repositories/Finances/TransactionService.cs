using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Domain.Features.Users.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Finances
{
    public class TransactionService : ITransactionRepository
    {
        private readonly AppDbContext _context;


        /// <summary>
        /// inyecta el contexto de la base de datos para poder realizar operaciones CRUD en la entidad Transaction
        /// </summary>
        /// <param name="context"></param>
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction request)
        {
            await _context.Transactions.AddAsync(request);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByUserIDAsync(Guid UserID)
        {
            return await _context.Transactions.Where(u => u.UserId == UserID)
                .Include(u => u.User)
                .Include(t => t.TransactionType)
                .Include(c => c.Category).ThenInclude(n => n.Nature)
                .OrderBy(o => o.CreatedAt).ThenBy(o => o.Category.Name)
                .ToListAsync();
        }

        /// <summary>
        /// retorna las transacciones del usuario por un rango de fecha y cuentaId
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CuentaId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Transaction?>> GetTransactionsByUserIDAndTimeAsync(Guid UserID, Guid CuentaId, DateTime FechaInicio, DateTime FechaFin)
        {
            return await _context.Transactions.Where(u => u.UserId == UserID && u.TransactionDate >= FechaInicio && u.TransactionDate <= FechaFin)
               .Include(u => u.User)
               .Include(t => t.TransactionType)
               .Include(c => c.Category).ThenInclude(n => n.Nature)
               .OrderBy(o => o.CreatedAt).ThenBy(o => o.Category.Name)
               .ToListAsync();
        }


        public async Task<Transaction?> GetTransactionByIDAsync(Guid id)
        {
            return await _context.Transactions.Where(u => u.Id == id)
                .Include(u => u.User)
                .Include(t => t.TransactionType)
                .Include(c => c.Category).Include(n => n.Category.Nature)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
