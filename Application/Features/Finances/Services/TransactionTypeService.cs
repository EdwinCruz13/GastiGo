using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Services
{
    public class TransactionTypeService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        /// <summary>
        /// constructor que inyecta el repositorio de tipos de transacción para que el servicio 
        /// pueda acceder a los datos relacionados con los tipos de transaccion
        /// </summary>
        /// <param name="transactionTypeRepository"></param>
        public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }


        /// <summary>
        /// retorna una lista de tipos de transacción, lo convierte a dto
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TransactionTypeDTO?>> GetAllTransactionTypesAsync()
        {
            var types = await _transactionTypeRepository.GetAllTransactionTypesAsync();
            return types.Select(t => t == null ? null : new TransactionTypeDTO
            {
                TransactionTypeID = t.Id,
                Name = t.Name,
                Code = t.Code,
                CurrentValue = t.CurrentValue
            });
        }

        /// <summary>
        /// retorna el item de tipo de transacción por su id, si no se encuentra devuelve null, 
        /// si se encuentra devuelve el tipo de transacción encontrado convertido a dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TransactionTypeDTO?> GetTransactionTypeByIdAsync(Guid id)
        {
            var type = await _transactionTypeRepository.GetTransactionTypeByIdAsync(id);
            return type == null ? null : new TransactionTypeDTO
            {
                TransactionTypeID = type.Id,
                Name = type.Name,
                Code = type.Code,
                CurrentValue = type.CurrentValue
            };
        }

        /// <summary>
        /// aumenta el valor actual del tipo de transacción en 1, 
        /// esto se utiliza para generar un nuevo código de transacción 
        /// único cada vez que se crea una nueva transacción,
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task IncrementCurrentValueAsync(Guid id)
        {
            await _transactionTypeRepository.IncrementCurrentValueAsync(id);
        }
    }
}
