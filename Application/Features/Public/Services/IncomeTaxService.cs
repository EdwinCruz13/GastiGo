using Application.Features.Public.DTOs;
using Application.Features.Public.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Services
{
    public class IncomeTaxService
    {
        private readonly IIncomeTaxRepository _incomeTaxRepository;
        public IncomeTaxService(IIncomeTaxRepository incomeTaxRepository)
        {
            _incomeTaxRepository = incomeTaxRepository;
        }

        /// <summary>
        /// retorna la lista de impuestos sobre la renta, cada uno con su rango mínimo y máximo, base, porcentaje y exceso. Si no hay impuestos registrados, devuelve una lista vacía.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IncomeTaxDTO?>> GetAllIncomeTaxesAsync()
        {
            var incomeTaxes = await _incomeTaxRepository.GetAllIncomeTax();
            return incomeTaxes.Select(it => (it == null) ? null : new IncomeTaxDTO
            {
                Id = it.Id,
                Min = it.Min,
                Max = it.Max,
                Base = it.Base,
                Percentage = it.Percentage,
                Excess = it.Excess
            });
        }

        /// <summary>
        /// retorna un impuesto sobre la renta específico basado en su ID. Si el impuesto no existe, devuelve null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IncomeTaxDTO?> GetIncomeTaxByIdAsync(int id)
        {
            var incomeTax = await _incomeTaxRepository.GetIncomeTaxByIdAsync(id);
            if (incomeTax == null) return null;
            return new IncomeTaxDTO
            {
                Id = incomeTax.Id,
                Min = incomeTax.Min,
                Max = incomeTax.Max,
                Base = incomeTax.Base,
                Percentage = incomeTax.Percentage,
                Excess = incomeTax.Excess
            };
        }
    }
}
