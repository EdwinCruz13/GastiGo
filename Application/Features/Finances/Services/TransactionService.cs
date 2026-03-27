using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Public.Interfaces;
using Application.Features.Users.DTOs;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;
using Domain.Features.Finances.ValueObject;

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

        /// <summary>
        /// inyecta servicios de transacciones
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ITransactionTypeRepository transactionTypeRepository,
            ICategoryRepository categoryRepository, IAccountRepository accountRepository, IIncomeTaxRepository taxRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _incomeTaxRepository = taxRepository;
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

                Account? fromAccount = null;
                Account? toAccount = null;


                if (user == null)
                    throw new Exception($"No se encontró ningún usuario con el ID {transaction.UserId}.");

                if (transactionType == null)
                    throw new Exception($"No se encontró ningún tipo de transacción con el ID {transaction.TransactionTypeId}.");

                if (category == null)
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
                if(transaction.FromAccountId != null)
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
                        transaction.ToAccountId.Value,
                        transaction.Amount,
                        "IN"
                    ));
                }

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
                    if (fromAccount?.Bank != null && fromAccount?.Bank.TransferFee > 0)
                    {
                        details.Add(new TransactionDetail(
                            transactionEntity.TransactionId,
                            transaction.FromAccountId.Value,
                            fromAccount.Bank.TransferFee * transaction.Amount,
                            "OUT"
                        ));
                    }


                }

                //asignar los detalles a la transacción
                foreach (var detail in details)
                {
                    transactionEntity.Details.Add(detail);
                }



                //guardar la transacción en el repositorio
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


    }
}
