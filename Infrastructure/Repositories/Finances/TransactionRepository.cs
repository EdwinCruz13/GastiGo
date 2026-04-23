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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;


        /// <summary>
        /// inyecta el contexto de la base de datos para poder realizar operaciones CRUD en la entidad Transaction
        /// </summary>
        /// <param name="context"></param>
        public TransactionRepository(AppDbContext context)
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
            return await _context.Transactions.Where(u => u.UserId == UserID && u.TransactionDate >= FechaInicio && u.TransactionDate <= FechaFin && u.AccountId == CuentaId)
               .Include(u => u.User)
               .Include(t => t.TransactionType)
               .Include(c => c.Category).ThenInclude(n => n.Nature)
               .Include(a => a.Account).ThenInclude(c => c.Currency)
               .OrderBy(o => o.TransactionDate).ThenBy(o => o.Category.Name)
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

        /// <summary>
        /// obtiene el balance de una cuenta específica, busca la última transacción asociada a la cuenta y devuelve su balance.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Transaction?> GetBalanceByAccountIdAsync(Guid userId, Guid accountId)
        {
            await this.Recalculate(userId, accountId);

            return await _context.Transactions.Where(u => u.AccountId == accountId && u.UserId == userId)
               .Include(u => u.User)
               .Include(t => t.TransactionType)
               .Include(a => a.Account).ThenInclude(c => c.Currency)
               .Include(c => c.Category).ThenInclude(n => n.Nature)
               .OrderBy(o => o.TransactionDate)
               .LastOrDefaultAsync();
        }


        /// <summary>
        /// permite guardar los cambios realizados en las transacciones y luego llama a un procedimiento almacenado para recalcular los balances de las transacciones, si ocurre algún error durante el proceso, se realiza un rollback de la transacción para mantener la integridad de los datos.
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// recalcula los balances de las transacciones de una cuenta específica
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task Recalculate(Guid userId, Guid accountId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Llama al procedimiento almacenado para recalcular el estado de cuetna de una cuenta específica
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL recalcular(@p0, @p1)",
                    userId,
                    accountId
                );
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

       
    }
}
