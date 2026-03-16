using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Users.DTOs;
using Domain.Features.Finances.Entities;
using Domain.Features.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// inyecta servicios de transacciones
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// crea una nueva transacción a partir de un DTO y la guarda en el repositorio
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddTransactionAsync(TransactionDTO transaction)
        {
            try
            {
                var transactionEntity = new Transaction(
                transaction.UserId,
                transaction.TransactionTypeId,
                transaction.CategoryId,
                transaction.AccountId,
                transaction.Amount,
                transaction.Description,
                transaction.TransactionDate,
                transaction.Reference,
                transaction.TransferGroupId
            );

                await _transactionRepository.AddAsync(transactionEntity);
                await _transactionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// retorna todas las transacciones de un usuario específico a partir de su ID, 
        /// mapeando cada transacción a un DTO de respuesta que incluye información detallada 
        /// sobre la transacción, el usuario, 
        /// el tipo de transacción, la categoría y la cuenta asociada
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<TransactionResponseDTO?>> GetAllTransactionsByUserIDAsync(Guid UserID)
        {
            try
            {
                var transaction = await _transactionRepository.GetAllTransactionsByUserIDAsync(UserID);
                return transaction.Select(t => t == null ? null : new TransactionResponseDTO
                {
                    TransactionID = t.TransactionId,
                    Amount = t.Amount,
                    Description = t.Description,
                    TransactionDate = t.TransactionDate,
                    Reference = t.Reference,
                    User = new UserDTO
                    {
                        UserID = t.User.UserID,
                        Username = t.User.Username,
                        Email = t.User.Email
                    },
                    TransactionType = new TransactionTypeDTO
                    {
                        TransactionTypeId = t.TransactionType.TransactionTypeId,
                        Name = t.TransactionType.Name,
                        Code = t.TransactionType.Code,
                        CurrentValue = t.TransactionType.CurrentValue
                    },
                    Category = new CategoryResponseDTO
                    {
                        CategoryId = t.Category.CategoryId,
                        Name = t.Category.Name,
                        Description = t.Category.Description,
                        Nature = new NatureDTO { NatureId = t.Category.Nature.Id, Name = t.Category.Nature.Name, Abbre = t.Category.Nature.Abbre }
                    },
                    Account = new AccountResponseDTO
                    {
                        AccountId = t.Account.AccountId,
                        Name = t.Account.Name,
                        Description = t.Account.Description,
                        AccountType = new AccountTypeDTO
                        {
                            AccountTypeId = t.Account.AccountType.AccountTypeId,
                            Name = t.Account.AccountType.Name,
                            Abbre = t.Account.AccountType.Abbre
                        },
                        Currecy = new CurrencyDTO
                        {
                            CurrencyId = t.Account.Currecy.CurrencyId,
                            Name = t.Account.Currecy.Name,
                            Symbol = t.Account.Currecy.Symbol,
                            Code = t.Account.Currecy.Code
                        },
                        Bank = new BankDTO
                        {
                            BankId = t.Account.Bank.BankId,
                            Name = t.Account.Bank.Name,
                            Abbre = t.Account.Bank.Abbre,
                            TransferFee = t.Account.Bank.TransferFee
                        },
                        Balance = t.Account.Balance
                    },
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// retorna la transaccion segun su ID, mapeando la transacción a un DTO de 
        /// respuesta que incluye información detallada
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TransactionResponseDTO?> GetTransactionByIDAsync(Guid Id)
        {
            try
            {
                var t = await _transactionRepository.GetByIDAsync(Id);
                return t == null ? null : new TransactionResponseDTO
                {
                    TransactionID = t.TransactionId,
                    Amount = t.Amount,
                    Description = t.Description,
                    TransactionDate = t.TransactionDate,
                    Reference = t.Reference,
                    User = new UserDTO
                    {
                        UserID = t.User.UserID,
                        Username = t.User.Username,
                        Email = t.User.Email
                    },
                    TransactionType = new TransactionTypeDTO
                    {
                        TransactionTypeId = t.TransactionType.TransactionTypeId,
                        Name = t.TransactionType.Name,
                        Code = t.TransactionType.Code,
                        CurrentValue = t.TransactionType.CurrentValue
                    },
                    Category = new CategoryResponseDTO
                    {
                        CategoryId = t.Category.CategoryId,
                        Name = t.Category.Name,
                        Description = t.Category.Description,
                        Nature = new NatureDTO { NatureId = t.Category.Nature.Id, Name = t.Category.Nature.Name, Abbre = t.Category.Nature.Abbre }
                    },
                    Account = new AccountResponseDTO
                    {
                        AccountId = t.Account.AccountId,
                        Name = t.Account.Name,
                        Description = t.Account.Description,
                        AccountType = new AccountTypeDTO
                        {
                            AccountTypeId = t.Account.AccountType.AccountTypeId,
                            Name = t.Account.AccountType.Name,
                            Abbre = t.Account.AccountType.Abbre
                        },
                        Currecy = new CurrencyDTO
                        {
                            CurrencyId = t.Account.Currecy.CurrencyId,
                            Name = t.Account.Currecy.Name,
                            Symbol = t.Account.Currecy.Symbol,
                            Code = t.Account.Currecy.Code
                        },
                        Bank = new BankDTO
                        {
                            BankId = t.Account.Bank.BankId,
                            Name = t.Account.Bank.Name,
                            Abbre = t.Account.Bank.Abbre,
                            TransferFee = t.Account.Bank.TransferFee
                        },
                        Balance = t.Account.Balance
                    },
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
