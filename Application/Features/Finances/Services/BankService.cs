using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Services
{
    public class BankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }


        /// <summary>
        /// retorna un banco por su id, si no se encuentra el banco, devuelve null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<BankDTO?> GetBankByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del banco no puede ser vacío.");

            var bank = await _bankRepository.GetBankByIdAsync(id);
            return bank == null ? null : new BankDTO
            {
                BankID = bank.Id,
                Name = bank.Name,
                Abbre = bank.Abbre,
                TransferFee = bank.TransferFee
            };
        }


        /// <summary>
        /// retorna todos los bancos, si no se encuentran bancos, devuelve una lista vacía
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BankDTO?>> GetAllBanksAsync()
        {
            var banks = await _bankRepository.GetAllBanksAsync();
            return banks.Select(b => b == null ? null : new BankDTO
            {
                BankID = b.Id,
                Name = b.Name,
                Abbre = b.Abbre,
                TransferFee = b.TransferFee
            });
        }

    }
}
