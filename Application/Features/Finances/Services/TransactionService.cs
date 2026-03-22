using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Users.DTOs;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;

namespace Application.Features.Finances.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// inyecta servicios de transacciones
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ITransactionTypeRepository transactionTypeRepository,
            ICategoryRepository categoryRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
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
                //validar que el usuario exista
                var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
                var transactionType = await _transactionTypeRepository.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
                var category = await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId);
                var account = await _accountRepository.GetAccountByIdAsync(transaction.AccountId);

                if (user == null)
                    throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");

                if (transactionType == null)
                    throw new Exception($"No se encontró ningún tipo de transacción con el ID {transaction.TransactionTypeId}.");

                if (category == null)
                    throw new Exception($"No se encontró ninguna categoría con el ID {transaction.CategoryId}.");

                if (account == null)
                    throw new Exception($"No se encontró ninguna cuenta con el ID {transaction.AccountId}.");


                //si todo bien, guardamos la transacción

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
                //validar que el usuario exista
                var user = await _userRepository.GetUserByIdAsync(UserID);

                if (user == null)
                   throw new ArgumentException($"No se encontró ningún usuario con el ID {UserID}.");


                var transaction = await _transactionRepository.GetTransactionsByUserIDAsync(UserID);
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
                        Currency = new CurrencyDTO
                        {
                            CurrencyId = t.Account.Currency.CurrencyId,
                            Name = t.Account.Currency.Name,
                            Symbol = t.Account.Currency.Symbol,
                            Code = t.Account.Currency.Code
                        },
                        Bank = new BankResponseDTO
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
                var t = await _transactionRepository.GetTransactionByIDAsync(Id);
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
                        Currency = new CurrencyDTO
                        {
                            CurrencyId = t.Account.Currency.CurrencyId,
                            Name = t.Account.Currency.Name,
                            Symbol = t.Account.Currency.Symbol,
                            Code = t.Account.Currency.Code
                        },
                        Bank = new BankResponseDTO
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
