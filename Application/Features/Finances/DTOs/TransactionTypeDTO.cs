using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    /// <summary>
    /// dto para transactiontype
    /// </summary>
    public class TransactionTypeDTO
    {
        public Guid TransactionTypeId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public int CurrentValue { get; set; }
    }
}
