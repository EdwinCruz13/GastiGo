using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Public.Interfaces;
using Application.Features.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// inyecta servicios de transacciones
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ITransactionTypeRepository transactionTypeRepository,
            ICategoryRepository categoryRepository, IAccountRepository accountRepository, IIncomeTaxRepository taxRepository, ICategoryParamRepository categoryParamRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _incomeTaxRepository = taxRepository;
            _categoryParamRepository = categoryParamRepository;
            _unitOfWork = unitOfWork;
        }



        /// <summary>
        /// crea el primer movimiento de una transacción, validando que el usuario y la cuenta existan en el sistema, y guardando los cambios en el repositorio para iniciar el proceso de creación de una transacción completa posteriormente.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddFirstMovementAsync(TransactionMovementDTO transaction)
        {
            try
            {
                //obtener la cuenta
                var account = await _accountRepository.GetAccountByIdAsync(transaction.AccountId);
                //obtener el usuario
                var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
                //obtener el tipo de transacción de primer movimiento
                var transactionTypes = await _transactionTypeRepository.GetAllTransactionTypesAsync();
                //obtener el tipo de transacción de primer movimiento
                var FirstMovTransactionType = transactionTypes.FirstOrDefault(t => t.Code == "FM");


                if (user == null)
                    throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");
                if (account == null)
                    throw new Exception($"No se encontró ninguna cuenta con el ID {transaction.AccountId}.");



                //crea la secuencia
                var sequence = FirstMovTransactionType.Next();
                var reference = TransactionReference.Create(FirstMovTransactionType.Code, sequence).ToString();


                //crear la transacción de primer movimiento con la información proporcionada en el DTO, incluyendo la referencia generada
                var transactionEntity = new Transaction(
                        transaction.UserId,
                        FirstMovTransactionType.TransactionTypeId,
                        null,
                        transaction.AccountId,
                        $"Primer Movimiento con secuencia: {reference}",
                        DateTime.UtcNow,
                        "IN",
                        0,
                        transaction.Amount,
                        transaction.Amount,
                        reference,
                        null
                );

                //guardar la transacción en el repositorio para iniciar el proceso
                await _transactionRepository.AddAsync(transactionEntity);
                await _transactionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// metodo que crea una nueva transacción a partir de un DTO, 
        /// validando la existencia del usuario, el tipo de transacción, la categoría y las cuentas involucradas
        /// usar unit of work para manejar la transacción y asegurar la integridad de los datos, 
        /// despiues de insertar entonces recalcular
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task AddTransactionAsync(TransactionDTO transaction)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Account? fromAccount = null;
                Account? toAccount = null;

                var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
                var transactionType = await _transactionTypeRepository.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
                var category = transaction.CategoryId.HasValue
                    ? await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId.Value)
                    : null;

                if (transaction.FromAccountId != null)
                    fromAccount = await _accountRepository.GetAccountByIdAsync(transaction.FromAccountId.Value);

                if (transaction.ToAccountId != null)
                    toAccount = await _accountRepository.GetAccountByIdAsync(transaction.ToAccountId.Value);

                if (user == null)
                    throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");

                if (transactionType == null)
                    throw new Exception($"No se encontró ningún tipo de transacción con el ID {transaction.TransactionTypeId}.");

                if (category == null && transaction.EntryType != "TRANSFER")
                    throw new Exception($"No se encontró ninguna categoría con el ID {transaction.CategoryId}.");

                var sequence = transactionType.Next();
                var reference = TransactionReference.Create(transactionType.Code, sequence).ToString();

                Guid? transferGroupId = null;

                if (transactionType.Code == "TRF")
                    transferGroupId = Guid.NewGuid();

                List<Transaction> transactionsToExecute = new();

                Guid? affectedAccountId = null;

                // INGRESO
                if (transactionType.Code == "INC")
                {
                    if (transaction.ToAccountId == null)
                        throw new Exception("Cuenta destino es requerida.");

                    affectedAccountId = transaction.ToAccountId;

                    var transactionEntity = new Transaction(
                        transaction.UserId,
                        transaction.TransactionTypeId,
                        transaction.CategoryId,
                        transaction.ToAccountId,
                        transaction.Description,
                        DateTime.UtcNow,
                        "IN",
                        0,
                        transaction.Amount,
                        0,
                        reference,
                        transferGroupId
                    );

                    // SI ES SALARIO, ENTONCES CREAR LAS PERCEPCIONES Y DEDUCCIONES CORRESPONDIENTES
                    if (category != null && category.isSalary)
                        transactionsToExecute = CreateListOfTransactionBySalary(
                            transaction.UserId,
                            Convert.ToDouble(transaction.Amount),
                            transaction.ToAccountId.Value
                        );

                    await _transactionRepository.AddAsync(transactionEntity);

                    foreach (var item in transactionsToExecute)
                        await _transactionRepository.AddAsync(item);
                }

                // GASTO
                if (transactionType.Code == "EXP")
                {
                    if (transaction.FromAccountId == null)
                        throw new Exception("Cuenta origen es requerida.");

                    affectedAccountId = transaction.FromAccountId;

                    var transactionEntity = new Transaction(
                        transaction.UserId,
                        transaction.TransactionTypeId,
                        transaction.CategoryId,
                        transaction.FromAccountId,
                        transaction.Description,
                        DateTime.UtcNow,
                        "OUT",
                        0,
                        transaction.Amount,
                        0,
                        reference,
                        transferGroupId
                    );

                    await _transactionRepository.AddAsync(transactionEntity);
                }

                // GUARDAR
                await _unitOfWork.SaveChangesAsync();

                // RECALCULAR
                if (affectedAccountId != null)
                {
                    await _unitOfWork.ExecuteSqlAsync(
                        "CALL recalcular(@p0, @p1)",
                        transaction.UserId,
                        affectedAccountId
                    );
                }

                //COMMIT FINAL
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


        /// <summary>
        /// crea una nueva transacción a partir de un DTO y la guarda en el repositorio
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        //public async Task AddTransactionAsync(TransactionDTO transaction)
        //{
        //    try
        //    {

        //        Account? fromAccount = null;
        //        Account? toAccount = null;
        //        // Category? category = null;

        //        //validar que el usuario exista
        //        var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
        //        //obtener la lista de trnasacciones del usuario
        //        var TransactionList = await _transactionRepository.GetTransactionsByUserIDAsync(transaction.UserId);
        //        //validar que el tipo de transacción exista
        //        var transactionType = await _transactionTypeRepository.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
        //        //obtener la información de la categoría si es que se proporcionó una categoría, ya que no es obligatoria para las transferencias
        //        var category = transaction.CategoryId.HasValue ? await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId.Value) : null;





        //        //obtener la informacion de las cuentas origne y destino
        //        if (transaction.FromAccountId != null)
        //            fromAccount = await _accountRepository.GetAccountByIdAsync(transaction.FromAccountId.Value);

        //        if (transaction.ToAccountId != null)
        //            toAccount = await _accountRepository.GetAccountByIdAsync(transaction.ToAccountId.Value);



        //        //crear una lista de transacciones a ejecutar en caso de salario
        //        List<Transaction> transactionsToExecute = new List<Transaction>();


        //        //validar que el usuario, el tipo de transacción y la categoría (si es que se proporcionó) existan en el sistema
        //        if (user == null)
        //            throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");

        //        if (transactionType == null)
        //            throw new Exception($"No se encontró ningún tipo de transacción con el ID {transaction.TransactionTypeId}.");

        //        if (category == null && transaction.EntryType != "TRANSFER")
        //            throw new Exception($"No se encontró ninguna categoría con el ID {transaction.CategoryId}.");



        //        //creamos la referencia usando el valueobject de referencia
        //        var sequence = transactionType.Next();
        //        var reference = TransactionReference.Create(transactionType.Code, sequence).ToString();



        //        //creamos el grupo de transferencia si es necesario
        //        Guid? transferGroupId = null;

        //        //si el tipo de transacción es una transferencia, generamos un nuevo GUID para el grupo de transferencia
        //        if (transactionType.Code == "TRF")
        //            transferGroupId = Guid.NewGuid();

        //        //si es un ingreso, el monto se registra en la cuenta de destino
        //        if (transactionType.Code == "INC")
        //        {
        //            if (transaction.ToAccountId == null)
        //                throw new Exception("Cuenta destino es requerida.");


        //            //crear la trnansacción de tipo ingreso con la información proporcionada en el DTO, incluyendo la referencia generada y el grupo de transferencia si es necesario
        //            var transactionEntity = new Transaction(
        //                transaction.UserId,
        //                transaction.TransactionTypeId,
        //                transaction.CategoryId,
        //                transaction.ToAccountId,
        //                transaction.Description,
        //                DateTime.UtcNow,
        //                "IN",
        //                0,
        //                transaction.Amount,
        //                0,
        //                reference,
        //                transferGroupId
        //            );


        //            //si es salario, crear las percepciones y deducciones correspondientes
        //            if (category != null && category.isSalary)
        //                transactionsToExecute = CreateListOfTransactionBySalary(transaction.UserId, Convert.ToDouble(transaction.Amount), transaction.ToAccountId.Value);


        //            await _transactionRepository.AddAsync(transactionEntity);
        //            foreach (var item in transactionsToExecute)
        //            {
        //                await _transactionRepository.AddAsync(item);
        //            }

        //        }


        //        // si es un gasto entonces deduccir
        //        if(transactionType.Code == "EXP")
        //        {
        //            if (transaction.FromAccountId == null)
        //                throw new Exception("Cuenta origen es requerida.");

        //            var transactionEntity = new Transaction(
        //                transaction.UserId,
        //                transaction.TransactionTypeId,
        //                transaction.CategoryId,
        //                transaction.FromAccountId,
        //                transaction.Description,
        //                DateTime.UtcNow,
        //                "OUT",
        //                0,
        //                transaction.Amount,
        //                0,
        //                reference,
        //                transferGroupId
        //            );

        //            //insertar gasto
        //            await _transactionRepository.AddAsync(transactionEntity);

        //        }


        //        //guardar los cambios en el repositorio

        //        await _transactionRepository.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}

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


                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// transaccion segun usuario, cuenta y fecha
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="cuentaId"></param>
        /// <param name="Fecha1"></param>
        /// <param name="Fecha2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<BalanceDTO?>> GetAllTransactionsByUserIdAndTimeAsync(Guid UserID, Guid cuentaId, string Fecha1, string Fecha2)
        {

            DateTime fechaInicio;
            DateTime fechaFin;

            try
            {
                //validar que el usuario exista
                var user = await _userRepository.GetUserByIdAsync(UserID);

                if (user == null)
                    throw new ArgumentException($"No se encontró ningún usuario con el ID {UserID}.");


                //validar cuenta exista
                var account = await _accountRepository.GetAccountByIdAsync(cuentaId);

                if (account == null)
                    throw new ArgumentException($"No se encontró ninguna cuenta con el ID {cuentaId}.");


                // Validar si vienen ambas fechas
                if (!string.IsNullOrWhiteSpace(Fecha1) && !string.IsNullOrWhiteSpace(Fecha2))
                {
                    // Validar que sean fechas válidas
                    bool esFecha1Valida = DateTime.TryParse(Fecha1, out fechaInicio);
                    bool esFecha2Valida = DateTime.TryParse(Fecha2, out fechaFin);

                    if (!esFecha1Valida || !esFecha2Valida)
                    {
                        throw new ArgumentException("Una o ambas fechas no son válidas.");
                    }

                    // Validar que fecha1 sea menor o igual que fecha2
                    if (fechaInicio > fechaFin)
                    {
                        throw new ArgumentException("La Fecha1 no puede ser mayor que Fecha2.");
                    }

                }
                else
                {
                    // Si vienen vacías o nulas → asignar valores por defecto
                    var hoy = DateTime.Now;

                    fechaInicio = new DateTime(hoy.Year, hoy.Month, 1); // primer día del mes
                    fechaFin = fechaInicio.AddMonths(1).AddDays(-1);    // último día del mes
                }



                //convierte a formato UTC ya que postgree maldito, solo reconoce UTC como si yo fuera militar
                fechaInicio = DateTime.SpecifyKind(fechaInicio, DateTimeKind.Utc).ToUniversalTime();
                fechaFin = DateTime.SpecifyKind(fechaFin, DateTimeKind.Utc).ToUniversalTime();


                var transactions = await _transactionRepository.GetTransactionsByUserIDAndTimeAsync(UserID, cuentaId, fechaInicio, fechaFin);


                return transactions.Select(t => t == null ? null : new BalanceDTO
                {
                    Description = t.Description,
                    TransactionDate = t.TransactionDate.ToString("dd/MM/yyyy"),
                    Reference = t.Reference,
                    EntryType = t.EntryType,
                    Balance = t.Balance,
                    PreviousBalance = t.PreviousBalance,
                    Amount = t.Amount,
                    Currency = t.Account.Currency.Symbol
                });



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



        /// <summary>
        /// metodo que obtiene el balance de una cuenta específica para un usuario determinado, validando la existencia del usuario y la cuenta
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<BalanceDTO?> GetBalance(Guid UserID, Guid AccountID)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(UserID);
                if (user == null)
                    throw new ArgumentException($"No se encontró ningún usuario con el ID {UserID}.");

                var account = await _accountRepository.GetAccountByIdAsync(AccountID);
                if (account == null)
                    throw new ArgumentException($"No se encontró ninguna cuenta con el ID {AccountID}.");


                var transaction = await _transactionRepository.GetBalanceByAccountIdAsync(UserID, AccountID);
                if (transaction == null)
                    throw new Exception($"No se encontró ninguna transacción para el usuario con ID {UserID} y la cuenta con ID {AccountID}.");


                //mapear a DTO
                return new BalanceDTO
                {
                    Description = transaction.Description,
                    TransactionDate = transaction.TransactionDate.ToString("dd/MM/yyyy"),
                    Reference = transaction.Reference,
                    EntryType = transaction.EntryType,
                    Balance = transaction.Balance,
                    PreviousBalance = transaction.PreviousBalance,
                    Amount = transaction.Amount,
                    Currency = transaction.Account.Currency.Symbol
                };


            }
            catch (Exception ex)
            {
                throw ex;
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




        // <summary>
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

            var transactionTypeByIncome = transactionType.Where(x => x.Code == "INC").FirstOrDefault();
            var transactionTypeByExpense = transactionType.Where(x => x.Code == "EXP").FirstOrDefault();




            //calcular los años de servicio
            DateTime hireFrom = Convert.ToDateTime(user.HiresDate.Value);
            int yearOfService = DateTime.Now.Year - hireFrom.Year;
            //si el usuario no ha cumplido un año completo, no se le asigna la bonificacion por año de servicio
            if (DateTime.Now < hireFrom.AddYears(yearOfService)) yearOfService--;


            //buscar las cuentas relacionada al salario
            var bonificacionPorAños = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "Años de servicios")?.CategoryId,
                Amount = 0
            };

            var bonificacionViaticoAlimentacion = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "Viático de alimentación")?.CategoryId,
                Amount = 0
            };

            var bonificacionPorTitulo = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "Titulo")?.CategoryId,
                Amount = 0
            };

            var seguroColectivo = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "Seguro colectivo")?.CategoryId,
                Amount = 0
            };

            var inss = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "INSS")?.CategoryId,
                Amount = 0
            };

            var ir = new TransactionMovementDTO
            {
                UserId = UserID,
                AccountId = accountId,
                CategoryId = categoryParam.FirstOrDefault(p => p.Category.Name == "IR")?.CategoryId,
                Amount = 0
            };


            if (categoryParam == null) return transactions;
            foreach (var param in categoryParam)
            {
                if (param.Category.Name == "Años de servicios")
                {

                    bonificacionPorAños.Amount = Convert.ToDecimal((salary * ((param.Value * yearOfService) / 100)));
                    var sequence = transactionTypeByIncome.Next();
                    var reference = TransactionReference.Create(transactionTypeByIncome.Code, sequence).ToString();
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                        param.CategoryId,
                        bonificacionPorAños.AccountId,
                        $"Bonificacion por año({param.Value * yearOfService})",
                        DateTime.UtcNow,
                        "IN",
                        0,
                        bonificacionPorAños.Amount,
                        0,
                        reference,
                        null
                    );

                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "Titulo")
                {
                    bonificacionPorTitulo.Amount = Convert.ToDecimal((salary * (param.Value / 100)));
                    var sequence = transactionTypeByIncome.Next();
                    var reference = TransactionReference.Create(transactionTypeByIncome.Code, sequence).ToString();
                    var transaction = new Transaction(
                       UserID,
                       transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                       param.CategoryId,
                       bonificacionPorTitulo.AccountId,
                       $"Titulo",
                       DateTime.UtcNow,
                       "IN",
                       0,
                       bonificacionPorTitulo.Amount,
                       0,
                       reference,
                       null
                   );

                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "Viático de alimentación")
                {
                    bonificacionViaticoAlimentacion.Amount = Convert.ToDecimal(param.Value);
                    var sequence = transactionTypeByIncome.Next();
                    var reference = TransactionReference.Create(transactionTypeByIncome.Code, sequence).ToString();
                    var transaction = new Transaction(
                      UserID,
                      transactionType.FirstOrDefault(t => t.Code == "INC")!.TransactionTypeId,
                      param.CategoryId,
                      bonificacionViaticoAlimentacion.AccountId,
                      $"Viático de alimentación",
                      DateTime.UtcNow,
                      "IN",
                      0,
                      bonificacionViaticoAlimentacion.Amount,
                      0,
                      reference,
                      null
                    );

                    //añadir a la lista
                    transactions.Add(transaction);
                }
            }

            //salario bruto
            decimal salarioBruto = Convert.ToDecimal(salary) + bonificacionPorAños.Amount + bonificacionPorTitulo.Amount;


            foreach (var param in categoryParam)
            {
                if (param.Category.Name == "INSS")
                {

                    inss.Amount = Convert.ToDecimal((param.Value / 100)) * salarioBruto;
                    var sequence = transactionTypeByExpense.Next();
                    var reference = TransactionReference.Create(transactionTypeByExpense.Code, sequence).ToString();
                    var transaction = new Transaction(
                         UserID,
                         transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                         param.CategoryId,
                         inss.AccountId,
                         $"Seguro INSS",
                         DateTime.UtcNow,
                         "OUT",
                         0,
                         inss.Amount,
                         0,
                         reference,
                         null
                     );
                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "IR")
                {
                    ir.Amount = CalcularIR(salarioBruto - inss.Amount);
                    var sequence = transactionTypeByExpense.Next();
                    var reference = TransactionReference.Create(transactionTypeByExpense.Code, sequence).ToString();
                    var transaction = new Transaction(
                         UserID,
                         transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                         param.CategoryId,
                         ir.AccountId,
                         "Impuesto sobre la renta",
                         DateTime.UtcNow,
                         "OUT",
                         0,
                         ir.Amount,
                         0,
                         reference,
                         null
                    );

                    //añadir a la lista
                    transactions.Add(transaction);
                }

                if (param.Category.Name == "Seguro colectivo")
                {
                    seguroColectivo.Amount = Convert.ToDecimal(param.Value);
                    var sequence = transactionTypeByExpense.Next();
                    var reference = TransactionReference.Create(transactionTypeByExpense.Code, sequence).ToString();
                    var transaction = new Transaction(
                        UserID,
                        transactionType.FirstOrDefault(t => t.Code == "EXP")!.TransactionTypeId,
                        param.CategoryId,
                        seguroColectivo.AccountId,
                         $"Seguro Colectivo",
                        DateTime.UtcNow,
                        "OUT",
                        0,
                        seguroColectivo.Amount,
                        0,
                        reference,
                        null
                    );
                    //añadir a la lista
                    transactions.Add(transaction);
                }
            }


            return transactions;
        }

        /// <summary>
        /// metodo privado que calcula el impuesto sobre la renta (IR) para un salario mensual dado, utilizando las tablas de impuestos obtenidas del repositorio de impuestos sobre la renta para determinar la cantidad de IR a deducir según los rangos de ingresos establecidos.
        /// </summary>
        /// <param name="salarioMensual"></param>
        /// <returns></returns>
        private decimal CalcularIR(decimal salarioMensual)
        {
            decimal anual = salarioMensual * 12;
            decimal IR = 0;
            var Taxes = _incomeTaxRepository.GetAllIncomeTax().Result;

            if (Taxes == null) return IR;
            foreach (var tax in Taxes)
            {
                if (anual > Convert.ToDecimal(tax?.Min) && anual <= Convert.ToDecimal(tax.Max))
                {
                    IR = (((anual - Convert.ToDecimal(tax.Excess)) * (Convert.ToDecimal(tax.Percentage) / 100)) + Convert.ToDecimal(tax.Base)) / 12;
                }
            }

            return IR;

        }





    }
}
