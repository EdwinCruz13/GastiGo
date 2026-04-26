using Application.Features.Dashboard.DTOs;
using Application.Features.Dashboard.Interfaces;
using Application.Features.Finances.Interfaces;
using Application.Features.Public.Interfaces;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;
using Domain.Features.Users.Entities;
using System.Linq.Expressions;
using System.Transactions;


namespace Application.Features.Dashboard.Services
{
    public class DashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExchangeRateRepository _ExchangeRateRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public DashboardService(IDashboardRepository dashboardRepository, IUserRepository userRepository, IExchangeRateRepository IExchangeRateRepository, IAccountRepository accountRepository, ICurrencyRepository currency)
        {
            _dashboardRepository = dashboardRepository;
            _userRepository = userRepository;
            _ExchangeRateRepository = IExchangeRateRepository;
            _accountRepository = accountRepository;
            _currencyRepository = currency;
        }


        /// <summary>
        /// metodo para obtener las transacciones de un usuario por año,
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="yearId"></param>
        /// <returns></returns>
        public async Task<DashboardYearDTO?> GetTIncomeAndExpenseByUserAndYearAsync(Guid UserID, Int32 yearId)
        {

            try
            {
                var user = await _userRepository.GetUserByIdAsync(UserID);
                if (user == null)
                    throw new ArgumentException($"No se encontró un usuario con el ID {UserID}.");


                var transactions = await _dashboardRepository.GetTIncomeAndExpenseByUserAndYearAsync(UserID, yearId);
                if (transactions == null || !transactions.Any())
                    throw new ArgumentException($"No se encontraron transacciones para el usuario con ID {UserID} en el año {yearId}.");



                var Monedas = await _currencyRepository.GetAllCurrenciesAsync();
                var cordoba = Monedas.FirstOrDefault(c => c.Symbol == "C$");
                var dolares = Monedas.FirstOrDefault(c => c.Symbol == "$");


                var currentExchage = await _ExchangeRateRepository.GetCurrentExchangeRateAsync(dolares.Id, cordoba.Id);
                if (currentExchage == null)
                    throw new ArgumentException($"No existe tasa de cambio actual.");

                // Crear una lista de meses del año (1-12)
                var months = Enumerable.Range(1, 12);


                // Agrupar las transacciones por naturaleza de la categoría (Ingreso, Gasto, Inversión) y luego por categoría
                var groups = transactions
               .GroupBy(t => t.Category?.Nature?.Name)
               .Select(g => new DashboardGroupDTO
               {
                   Name = g.Key,
                   Categories = g
                       .GroupBy(t => t.Category?.Name)
                       .Select(cat => new DashboardCategoryDTO
                       {
                           Name = cat.Key,

                           Values = months.Select(m => new MonthlyValueDTO
                           {
                               Month = m,

                               Amount = cat
                                   .Where(t => t.TransactionDate.Month == m)
                                   .Sum(t => t.EntryType == "IN"
                                       ? t.Account.Currency.Symbol == "$" ? t.Amount * currentExchage.Value : t.Amount
                                       : -(t.Account.Currency.Symbol == "$" ? t.Amount * currentExchage.Value : t.Amount))
                                   
                           }).ToList() ?? new List<MonthlyValueDTO>()
                       }).ToList() ?? new List<DashboardCategoryDTO>()
               }).ToList();


                // 5. Retornar DTO final
                return new DashboardYearDTO
                {
                    Year = yearId,
                    Groups = groups
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// obtiene el ahorro total de un usuario por año, se categoriza por mes y se devuelve un objeto con la información de las transacciones, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="yearId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<DashboardYearDTO?> GetTSavingsByUserAndYearAsync(Guid UserID, Int32 yearId)
        {

            try
            {
                var user = await _userRepository.GetUserByIdAsync(UserID);
                if (user == null)
                    throw new ArgumentException($"No se encontró un usuario con el ID {UserID}.");


                //buscar la cuenta de ahorro del usuario
                var accounts = await _accountRepository.GetAllAccountsByUserIDAsync(UserID);
                var savingsAccount = accounts.FirstOrDefault(a => a.AccountType.Abbre == "TYPE-SAVS");


                var Monedas = await _currencyRepository.GetAllCurrenciesAsync();
                var cordoba = Monedas.FirstOrDefault(c => c.Symbol == "C$");
                var dolares = Monedas.FirstOrDefault(c => c.Symbol == "$");


                if (savingsAccount == null)
                    throw new ArgumentException($"No se encontró una cuenta de ahorro para el usuario con ID {UserID}.");


                //buscar las transacciones de ahorro del usuario por año
                var transactions = await _dashboardRepository.GetTSavingsByUserAndYearAsync(UserID, savingsAccount.AccountId, yearId);
                if (transactions == null || !transactions.Any())
                    throw new ArgumentException($"No se encontraron transacciones en la cuenta de ahorro para el usuario con ID {UserID} en el año {yearId}.");



                var currentExchage = await _ExchangeRateRepository.GetCurrentExchangeRateAsync(dolares.Id, cordoba.Id);
                if(currentExchage == null)
                    throw new ArgumentException($"No existe tasa de cambio actual.");

                // Crear una lista de meses del año (1-12)
                var months = Enumerable.Range(1, 12);


                // Agrupar las transacciones por naturaleza de la categoría (Ingreso, Gasto, Inversión) y luego por categoría
                var groups = transactions
               .GroupBy(t => t.Account.AccountType.Name)
               .Select(g => new DashboardGroupDTO
               {
                   Name = g.Key,
                   Categories = g
                       .GroupBy(t => t.Account.AccountType.Name)
                       .Select(cat => new DashboardCategoryDTO
                       {
                           Name = g.Key, //?? "Savings",
                           //Name = "Savings",

                           Values = months.Select(m => new MonthlyValueDTO
                           {
                               Month = m,

                               Amount = cat
                                   .Where(t => t.TransactionDate.Month == m)
                                   .Sum(t => t.EntryType == "IN"
                                       ? t.Account.Currency.Symbol == "$" ? t.Amount * currentExchage.Value : t.Amount
                                       : - (t.Account.Currency.Symbol == "$" ? t.Amount * currentExchage.Value : t.Amount))
                           }).ToList() ?? new List<MonthlyValueDTO>()
                       }).ToList() ?? new List<DashboardCategoryDTO>()
               }).ToList();


                // 5. Retornar DTO final
                return new DashboardYearDTO
                {
                    Year = yearId,
                    Groups = groups
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// obtiene la inversión total de un usuario por año, se categoriza por mes y se devuelve un objeto con la información de las transacciones, se utiliza para mostrar la información en el dashboard del usuario
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="yearId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<DashboardYearDTO?> GetTInvestmentByUserAndYearAsync(Guid UserID, Int32 yearId)
        {

            try
            {
                var user = await _userRepository.GetUserByIdAsync(UserID);
                if (user == null)
                    throw new ArgumentException($"No se encontró un usuario con el ID {UserID}.");


                //buscar la cuenta de ahorro del usuario
                var accounts = await _accountRepository.GetAllAccountsByUserIDAsync(UserID);
                var InvestmentAccount = accounts.FirstOrDefault(a => a.AccountType.Abbre == "TYPE-INVS");


                if (InvestmentAccount == null)
                    throw new ArgumentException($"No se encontró una cuenta de ahorro para el usuario con ID {UserID}.");


                //buscar las transacciones de ahorro del usuario por año
                var transactions = await _dashboardRepository.GetTSavingsByUserAndYearAsync(UserID, InvestmentAccount.AccountId, yearId);
                if (transactions == null || !transactions.Any())
                    throw new ArgumentException($"No se encontraron transacciones en la cuenta de ahorro para el usuario con ID {UserID} en el año {yearId}.");


                // Crear una lista de meses del año (1-12)
                var months = Enumerable.Range(1, 12);


                // Agrupar las transacciones por naturaleza de la categoría (Ingreso, Gasto, Inversión) y luego por categoría
                var groups = transactions
               .GroupBy(t => t.Account.AccountType.Name)
               .Select(g => new DashboardGroupDTO
               {
                   Name = g.Key,
                   Categories = g
                       .GroupBy(t => t.Account.AccountType.Name)
                       .Select(cat => new DashboardCategoryDTO
                       {
                           Name = g.Key, //?? "Savings",
                           //Name = "Savings",

                           Values = months.Select(m => new MonthlyValueDTO
                           {
                               Month = m,

                               Amount = cat
                                   .Where(t => t.TransactionDate.Month == m)
                                   .Sum(t => t.EntryType == "IN"
                                       ? t.Amount
                                       : -t.Amount)
                           }).ToList() ?? new List<MonthlyValueDTO>()
                       }).ToList() ?? new List<DashboardCategoryDTO>()
               }).ToList();


                // 5. Retornar DTO final
                return new DashboardYearDTO
                {
                    Year = yearId,
                    Groups = groups
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
