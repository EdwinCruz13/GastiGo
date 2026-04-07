using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Public.Interfaces;
using Application.Features.Users.DTOs;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;
using Domain.Features.Finances.ValueObject;
using Domain.Features.Users.Entities;

using Transaction = Domain.Features.Finances.Entities.Transaction;

namespace Application.Features.Finances.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IIncomeTaxRepository _incomeTaxRepository;
        private readonly ICategoryParamRepository _categoryParamRepository;

        /// <summary>
        /// inyecta servicios de transacciones
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ITransactionTypeRepository transactionTypeRepository,
            ICategoryRepository categoryRepository, IAccountRepository accountRepository, IIncomeTaxRepository taxRepository, ICategoryParamRepository categoryParamRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _incomeTaxRepository = taxRepository;
            _categoryParamRepository = categoryParamRepository;
        }

        /// <summary>
        /// crea una lista de transacciones relacionadas con el pago de salario de un usuario, calculando las bonificaciones y deducciones correspondientes según los parámetros de categoría definidos en el sistema.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="salary"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private List<Transaction> CreateListOfTransactionBySalary(Guid UserID, double salary, Guid accountId)
        {
            List<Transaction> transactions = new List<Transaction>();

            //obtener los parametros de categoria para calcular las bonificaciones
            var user = _userRepository.GetUserByIdAsync(UserID).Result;
            var categoryParam = _categoryParamRepository.GetAllCategoryParamsAsync().Result;
            var transactionType = _transactionTypeRepository.GetAllTransactionTypesAsync().Result;


            //calcular los años de servicio
            DateTime hireFrom = Convert.ToDateTime(user.HiresDate.Value);
            int yearOfService = DateTime.Now.Year - hireFrom.Year;
            //si el usuario no ha cumplido un año completo, no se le asigna la bonificacion por año de servicio
            if (DateTime.Now < hireFrom.AddYears(yearOfService)) yearOfService--;

            //calcular el antiguedad
            double bonificacionPorAños = 0;
            //calcular el titulo
            double bonificacionPorTitulo = 0;
            //viatico de alimentacion
            double bonificacionViaticoAlimentacion = 0;


            //obtener seguro colectivo
            double seguroColectivo = 0;
            //calcular INSS
            double inss = 0;
            //calcular el impuesto sobre la renta
            double ir = 0;





            if (categoryParam == null) return transactions;
            foreach (var param in categoryParam)
            {
                var details = new List<TransactionDetail>();
                if (param.Category.Name == "Años de servicios")
                {

                    bonificacionPorAños = (salary * ((param.Value * yearOfService) / 100));
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                        param.CategoryId,
                        $"Bonificacion por año({param.Value * yearOfService})",
                        DateTime.UtcNow,
                        "",
                        null
                    );


                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        bonificacionPorAños,
                        "IN"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if(param.Category.Name == "Titulo")
                {
                    bonificacionPorTitulo = (salary * (param.Value/100));
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                        param.CategoryId,
                        $"Titulo",
                        DateTime.UtcNow,
                        "",
                        null
                    );
                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        bonificacionPorTitulo,
                        "IN"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "Viático de alimentación")
                {
                    bonificacionViaticoAlimentacion = param.Value;
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                        param.CategoryId,
                        $"Viático de alimentación",
                        DateTime.UtcNow,
                        "",
                        null
                    );
                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        bonificacionViaticoAlimentacion,
                        "IN"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }
            }

            //salario bruto
            double salarioBruto = salary + bonificacionPorAños + bonificacionPorTitulo;


            foreach (var param in categoryParam)
            {
                var details = new List<TransactionDetail>();
                if (param.Category.Name == "INSS")
                {

                    inss = (param.Value / 100) * salarioBruto;
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                        param.CategoryId,
                        $"Seguro INSS",
                        DateTime.UtcNow,
                        "",
                        null
                    );
                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        inss,
                        "OUT"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "IR")
                {
                    ir = CalcularIR(salarioBruto - inss);
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                        param.CategoryId,
                        "Impuesto sobre la renta",
                        DateTime.UtcNow,
                        "",
                        null
                    );
                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        ir,
                        "OUT"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "Seguro colectivo")
                {
                    seguroColectivo = param.Value;
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                        param.CategoryId,
                        $"Seguro Colectivo",
                        DateTime.UtcNow,
                        "",
                        null
                    );
                    details.Add(new TransactionDetail(
                        transaction.TransactionId,
                        accountId,
                        seguroColectivo,
                        "OUT"
                    ));
                    foreach (var detail in details) transaction.Details.Add(detail);
                    //añadir a la lista
                    transactions.Add(transaction);
                }
            }




           
            




            return transactions;
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

                Account? fromAccount = null;
                Account? toAccount = null;
                Category? category = null;

                //validar que el usuario exista
                var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
                var transactionType = await _transactionTypeRepository.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
                category = transaction.CategoryId.HasValue? await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId.Value): null;


                //crear una lista de transacciones a ejecutar
                List<Transaction> transactionsToExecute = new List<Transaction>();

               


                if (user == null)
                    throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");

                if (transactionType == null)
                    throw new Exception($"No se encontró ningún tipo de transacción con el ID {transaction.TransactionTypeId}.");

                if (category == null && transaction.EntryType != "TRANSFER")
                    throw new Exception($"No se encontró ninguna categoría con el ID {transaction.CategoryId}.");



                //creamos la referencia usando el valueobject de referencia
                var sequence = transactionType.Next();
                var reference = TransactionReference.Create(transactionType.Code, sequence).ToString();



                //creamos el grupo de transferencia si es necesario
                Guid? transferGroupId = null;

                //si el tipo de transacción es una transferencia, generamos un nuevo GUID para el grupo de transferencia
                if (transactionType.Code == "TRF")
                    transferGroupId = Guid.NewGuid();


                //crear la trnansacción
                var transactionEntity = new Transaction(
                    transaction.UserId,
                    transaction.TransactionTypeId,
                    transaction.CategoryId,
                    transaction.Description,
                    DateTime.UtcNow,
                    reference,
                    transferGroupId
                );

                //creamos el detalle de la transacción
                var details = new List<TransactionDetail>();


                //obtener la informacion de las cuentas origne y destino
                if (transaction.FromAccountId != null)
                    fromAccount = await _accountRepository.GetAccountByIdAsync(transaction.FromAccountId.Value);

                if (transaction.ToAccountId != null)
                    toAccount = await _accountRepository.GetAccountByIdAsync(transaction.ToAccountId.Value);


                //creamos el detalle de la transacción segun el tipo de transacción

                //si es un ingreso, el monto se registra en la cuenta de destino
                if (transactionType.Code == "INC")
                {
                    if (transaction.ToAccountId == null)
                        throw new Exception("Cuenta destino es requerida.");

                    details.Add(new TransactionDetail(
                            transactionEntity.TransactionId,
                            toAccount.AccountId,
                            transaction.Amount,
                            "IN"
                     ));

                    //asignar el salario como detalle
                    foreach (var detail in details)
                    {
                        transactionEntity.Details.Add(detail);
                    }


                    if(category.isSalary == true)
                    {
                        //guardar la transacción principal para obtener el ID y poder asignarlo a los detalles
                        await _transactionRepository.AddAsync(transactionEntity);


                        //ahora anadir las transacciones relacionadas con el salario, como bonificaciones y deducciones, si es que la categoria tiene el parametro de aplicar salario
                        var ListOfTransactionBySalary = CreateListOfTransactionBySalary(transaction.UserId, transaction.Amount, transaction.ToAccountId.Value);

                        foreach (var t in ListOfTransactionBySalary)
                        {
                            await _transactionRepository.AddAsync(t);
                        }
                    }
                    else
                    {
                        await _transactionRepository.AddAsync(transactionEntity);
                    }


                   

                }

                else
                {


                    //si es gasto, el monto se registra en la cuenta de origen
                    if (transactionType.Code == "EXP")
                    {
                        if (transaction.FromAccountId == null)
                            throw new Exception("Cuenta origen es requerida.");

                        details.Add(new TransactionDetail(
                            transactionEntity.TransactionId,
                            transaction.FromAccountId.Value,
                            transaction.Amount,
                            "OUT"
                        ));
                    }

                    //si es transferencia, el monto se registra tanto en la cuenta de origen como en la cuenta de destino
                    //se debe de registrar la comision como gasto adicional en la cuenta origne
                    if (transactionType.Code == "TRF")
                    {
                        if (transaction.FromAccountId == null)
                            throw new Exception("Cuenta origen es requerida.");

                        if (transaction.FromAccountId == null || transaction.ToAccountId == null)
                            throw new Exception("Cuentas origen y destino requeridas.");

                        if (transaction.FromAccountId == transaction.ToAccountId)
                            throw new Exception("No puedes transferir a la misma cuenta.");

                        // salida
                        details.Add(new TransactionDetail(
                            transactionEntity.TransactionId,
                            transaction.FromAccountId.Value,
                            transaction.Amount,
                            "OUT"
                        ));

                        // entrada
                        details.Add(new TransactionDetail(
                            transactionEntity.TransactionId,
                            transaction.ToAccountId.Value,
                            transaction.Amount,
                            "IN"
                        ));

                        // comisión
                        if(transactionType.Code != "TRF")
                            if (fromAccount?.Bank != null && toAccount?.Bank.TransferFee > 0)
                            {
                                details.Add(new TransactionDetail(
                                    transactionEntity.TransactionId,
                                    transaction.FromAccountId.Value,
                                    fromAccount.Bank.TransferFee * transaction.Amount,
                                    "OUT"
                                ));
                            }


                    }


                    //asignar el salario como detalle
                    foreach (var detail in details)
                    {
                        transactionEntity.Details.Add(detail);
                    }
                    //guardar la transacción en el repositorio
                    await _transactionRepository.AddAsync(transactionEntity);
                }


                //guardar los cambios en el repositorio
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
                    TransactionId = t.TransactionId,

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
                    Category = (t.Category == null) ? null : new CategoryResponseDTO
                    {
                        CategoryId = t.Category.CategoryId,
                        Name = t.Category.Name,
                        Description = t.Category.Description,
                        Nature = new NatureDTO { NatureId = t.Category.Nature.Id, Name = t.Category.Nature.Name, Abbre = t.Category.Nature.Abbre }
                    },
                    Amount = t.Details.Select( d => d.Amount).FirstOrDefault(),
                    EntryType = t.Details.Select(d => d.EntryType).FirstOrDefault() ?? string.Empty,
                    Account = t.Details.Select(d => d.Account == null ? null : new AccountResponseDTO
                    {
                        Description = d.Account.Description,
                        Bank = (d.Account.Bank == null) ? null : new BankResponseDTO { Name = d.Account.Bank.Name, Abbre = d.Account.Bank.Abbre, BankId = d.Account.Bank.BankId, TransferFee = d.Account.Bank.TransferFee },
                        Currency = new CurrencyDTO { Name = d.Account.Currency.Name, Symbol = d.Account.Currency.Symbol, Code = d.Account.Currency.Code, CurrencyId = d.Account.Currency.CurrencyId },
                        Balance = d.Account.Balance
                    }).FirstOrDefault(),

                    Detail = new TransactionDetailResponseDTO
                    {
                        TransactionDetailId = t.Details.Select(d => d.TransactionDetailId).FirstOrDefault(),
                        Amount = t.Details.Select(d => d.Amount).FirstOrDefault(),
                        EntryType = t.Details.Select(d => d.EntryType).FirstOrDefault() ?? string.Empty,
                        Account = (t.Details.Select(d => d.Account == null ? null : new AccountResponseDTO
                        {
                            Description = d.Account.Description,
                            Bank = (d.Account.Bank == null) ? null : new BankResponseDTO { Name = d.Account.Bank.Name, Abbre = d.Account.Bank.Abbre, BankId = d.Account.Bank.BankId, TransferFee = d.Account.Bank.TransferFee},
                            Currency = new CurrencyDTO { Name = d.Account.Currency.Name, Symbol = d.Account.Currency.Symbol, Code = d.Account.Currency.Code, CurrencyId = d.Account.Currency.CurrencyId},
                            Balance = d.Account.Balance
                        })).FirstOrDefault()
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
                    TransactionId = t.TransactionId,
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

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        private double CalcularIR(double salarioMensual)
        {
            double anual = salarioMensual * 12;
            double IR = 0;
            var Taxes = _incomeTaxRepository.GetAllIncomeTax().Result;

            if (Taxes == null) return IR;
            foreach (var tax in Taxes)
            {
                if (anual > tax?.Min && anual <= tax.Max)
                {
                    IR = (((anual - tax.Excess) * (tax.Percentage/100)) + tax.Base) / 12;
                }
            }

            return IR;

            //if (anual <= 100000)
            //    return 0;

            //if (anual <= 200000)
            //    return ((anual - 100000) * 0.15) / 12;

            //if (anual <= 350000)
            //    return ((anual - 200000) * 0.20 + 15000) / 12;

            //if (anual <= 500000)
            //    return ((anual - 350000) * 0.25 + 45000) / 12;

            //return ((anual - 500000) * 0.30 + 82500) / 12;
        }





     }
}
