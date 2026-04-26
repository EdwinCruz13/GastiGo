using Application.Features.Dashboard.Interfaces;
using Application.Features.Finances.Interfaces;
using Application.Features.UnitOfWork;
using Application.Features.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Services
{

    /// <summary>
    /// este servicio permite recalcular 
    /// </summary>
    public class BalanceService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private IUnitOfWork _unitOfWork;

        public BalanceService(ITransactionRepository transactionRepository, IUserRepository userRepository, IAccountRepository accountRepository ,IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// permite recalcular el balance de todas las cuentas de un usuario, se obtiene todas las cuentas del usuario y se recalcula el balance de cada cuenta utilizando las transacciones asociadas a cada cuenta, se utiliza para mantener el balance actualizado 
        /// en caso de que se hayan realizado cambios en las transacciones o en las cuentas.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RecalculateBalanceAsync(Guid userId)
        {
            try
            {

                //obtener todas las cuentas disponible del usuario
                var accounts = await _accountRepository.GetAllAccountsByUserIDAsync(userId);

                if(accounts == null)
                    throw new Exception($"No se encontraron cuentas para el usuario con ID {userId}.");



                // Recalcular el balance para cada cuenta
                foreach (var account in accounts)
                    await _transactionRepository.Recalculate(userId, account.AccountId);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
