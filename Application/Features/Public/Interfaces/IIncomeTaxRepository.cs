using Domain.Features.Public.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Interfaces
{
    public interface IIncomeTaxRepository
    {
        Task<IEnumerable<IncomeTax?>> GetAllIncomeTax();
        Task<IncomeTax?> GetIncomeTaxByIdAsync(Int32 id);
    }
}
