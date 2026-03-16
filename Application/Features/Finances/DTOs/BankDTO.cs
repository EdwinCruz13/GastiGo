using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class BankDTO
    {
        public Guid BankId { get; set; }
        public String Name { get; set; }
        public String Abbre { get; set; }
        public double TransferFee { get; set; }
    }
}
