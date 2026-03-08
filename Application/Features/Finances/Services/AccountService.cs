using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Users.DTOs;
using Domain.Features.Finances.Entities;
using System.ComponentModel;


namespace Application.Features.Finances.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// busca la cuenta con el ID asociado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<AccountResponseDTO?> GetAccountByIDAsync(Guid id) {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Debe de ingresar un ID valido");

                //busca la cuenta y luego la transforma en el dto
                var account = await _accountRepository.GetAccountByIdAsync(id);
                return account == null ? null : new AccountResponseDTO
                {
                    AccountID = account.AccountID,
                    User = new UserDTO
                    {
                        UserID = account.User.UserID,
                        Username = account.User.Username,
                        Email = account.User.Email
                    },
                    AccountType = new AccountTypeDTO
                    {
                        AccountTypeID = account.AccountType.AccountTypeID,
                        Name = account.AccountType.Name,
                        Abbre = account.AccountType.Abbre
                    },
                    Currecy = new CurrencyDTO
                    {
                        CurrencyID = account.CurrecyID,
                        Name = account.Currecy.Name,
                        Symbol = account.Currecy.Symbol,
                        Code = account.Currecy.Code
                    },
                    Bank = new BankDTO
                    {
                        BankID = account.Bank.BankID,
                        Name = account.Bank.Name,
                        Abbre = account.Bank.Abbre,
                        TransferFee = account.Bank.TransferFee
                    },
                    Name = account.Name,
                    Description = account.Description,
                    Balance = account.Balance
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// busca todas las cuentas bancarias o de efectivo asociadas a un usuario específico,
        /// utilizando el UserID como criterio de búsqueda.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountResponseDTO?>> GetAllAccountsByUserIDAsync(Guid UserID)
        {

            if (UserID == Guid.Empty)
                throw new ArgumentException("Debe de ingresar un UserID valido");


            try
            {
                //busca la cuenta asociada
                var accounts = await _accountRepository.GetAllAccountsByUserIDAsync(UserID);

                //retorna el dto
                var accountResponseDTOs = accounts.Select(account => account == null ? null : new AccountResponseDTO
                {
                    AccountID = account.AccountID,
                    User = new UserDTO
                    {
                        UserID = account.User.UserID,
                        Username = account.User.Username,
                        Email = account.User.Email
                    },
                    AccountType = new AccountTypeDTO
                    {
                        AccountTypeID = account.AccountType.AccountTypeID,
                        Name = account.AccountType.Name,
                        Abbre = account.AccountType.Abbre
                    },
                    Currecy = new CurrencyDTO
                    {
                        CurrencyID = account.CurrecyID,
                        Name = account.Currecy.Name,
                        Symbol = account.Currecy.Symbol,
                        Code = account.Currecy.Code
                    },
                    Bank = new BankDTO
                    {
                        BankID = account.Bank.BankID,
                        Name = account.Bank.Name,
                        Abbre = account.Bank.Abbre,
                        TransferFee = account.Bank.TransferFee
                    },
                    Name = account.Name,
                    Description = account.Description,
                    Balance = account.Balance
                }).ToList();
                return accountResponseDTOs;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// anade un nuevo cuenta bancaria o efectivo a la bd
        /// por defecto el balance es 0
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task CreateAccountAsync(AccountDTO account)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(account.Name))
                    throw new ArgumentException("El nombre de la cuenta no puede estar vacío.");

                if (string.IsNullOrEmpty(account.Description))
                    throw new ArgumentException("Debe de ingresar una descripcion");

                if (account.Balance < 0)
                    throw new ArgumentException("El balance inicial no puede ser negativo.");

                if (account.UserID == null)
                    throw new ArgumentException("Debe de ingresar un usuario valido");

                if (account.AccountTypeID == null)
                    throw new ArgumentException("Debe de ingresar un tipo de cuenta valido");

                if (account.BankID == null)
                    throw new ArgumentException("Debe de ingresar el tipo de banco");


                //crear la nueva cuenta
                var Account = new Account(account.UserID, account.AccountTypeID, account.CurrencyID, account.BankID, account.Name, account.Description, account.Balance);
                await _accountRepository.AddAsync(Account);
                await _accountRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// actualiza la cuenta contable existente, validando que el nombre no esté vacío y el balance no sea negativo
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateAccountAsync(AccountDTO account)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(account.Name))
                    throw new ArgumentException("El nombre de la cuenta no puede estar vacío.");
                if (string.IsNullOrEmpty(account.Description))
                    throw new ArgumentException("Debe de ingresar una descripcion");
                if (account.Balance < 0)
                    throw new ArgumentException("El balance inicial no puede ser negativo.");
                if (account.UserID == null)
                    throw new ArgumentException("Debe de ingresar un usuario valido");
                if (account.AccountTypeID == null)
                    throw new ArgumentException("Debe de ingresar un tipo de cuenta valido");
                if (account.BankID == null)
                    throw new ArgumentException("Debe de ingresar el tipo de banco");


                //actualiza la cuenta contable
                var newAccount = new Account(account.UserID, account.AccountTypeID, account.CurrencyID, account.BankID, account.Name, account.Description, account.Balance);
                await _accountRepository.UpdateAsync(newAccount);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
