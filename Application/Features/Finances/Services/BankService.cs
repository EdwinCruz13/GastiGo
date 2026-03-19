using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
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
        /// guarda un nuevo banco en la base de datos, si el objeto de solicitud del banco es nulo, lanza una excepción ArgumentNullException,
        /// </summary>
        /// <param name="bankRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateBankAsync(BankDTO bankRequest)
        {
            if (bankRequest == null)
                throw new ArgumentNullException(nameof(bankRequest), "El objeto de solicitud del banco no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(bankRequest.Name))
                throw new ArgumentException("El nombre del banco no puede estar vacío.", nameof(bankRequest.Name));

            if (string.IsNullOrWhiteSpace(bankRequest.Abbre))
                throw new ArgumentException("La abreviatura del banco no puede estar vacía.", nameof(bankRequest.Abbre));

            if (bankRequest.TransferFee <= 0)
                throw new ArgumentException("La tafira de transferencia no puede ser igual o menor que 0.", nameof(bankRequest.TransferFee));


            // crea un nuevo banco
            var bank = new Bank(bankRequest.Name.ToUpper(), bankRequest.Abbre.ToUpper(), bankRequest.TransferFee, bankRequest.imgURL);
            await _bankRepository.AddAsync(bank);
            await _bankRepository.SaveChangesAsync();

        }

        /// <summary>
        /// actualiza un banco existente en la base de datos, si el banco no existe, lanza una excepción ArgumentException,
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bankRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>

        public async Task UpdateBankAsync(Guid id, BankDTO bankRequest)
        {
            //buscar el banco por su id
            var existingBank = await _bankRepository.GetBankByIdAsync(id);

            if (existingBank == null)
                throw new ArgumentException("El banco no existe.");


            if (string.IsNullOrWhiteSpace(bankRequest.Name))
                throw new ArgumentException("El nombre del banco no puede estar vacío.", nameof(bankRequest.Name));
            if (string.IsNullOrWhiteSpace(bankRequest.Abbre))
                throw new ArgumentException("La abreviatura del banco no puede estar vacía.", nameof(bankRequest.Abbre));
            if (bankRequest.TransferFee <= 0)
                throw new ArgumentException("La tafira de transferencia no puede ser igual o menor que 0.", nameof(bankRequest.TransferFee));


            //actualizar el banco
            existingBank.Update(bankRequest.Name.ToUpper(), bankRequest.Abbre.ToUpper(), bankRequest.TransferFee, bankRequest.imgURL);

            await _bankRepository.SaveChangesAsync();


        }


        /// <summary>
        /// retorna un banco por su id, si no se encuentra el banco, devuelve null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<BankResponseDTO?> GetBankByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del banco no puede ser vacío.");

            var bank = await _bankRepository.GetBankByIdAsync(id);
            return bank == null ? null : new BankResponseDTO
            {
                BankId = bank.Id,
                Name = bank.Name,
                Abbre = bank.Abbre,
                TransferFee = bank.TransferFee,
                imgURL = bank.ImgURL
            };
        }


        /// <summary>
        /// retorna todos los bancos, si no se encuentran bancos, devuelve una lista vacía
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BankResponseDTO?>> GetAllBanksAsync()
        {
            var banks = await _bankRepository.GetAllBanksAsync();
            return banks.Select(b => b == null ? null : new BankResponseDTO
            {
                BankId = b.Id,
                Name = b.Name,
                Abbre = b.Abbre,
                TransferFee = b.TransferFee,
                imgURL = b.ImgURL
            });
        }

    }
}
