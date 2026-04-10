using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class BalanceDTO
    {
        //public Guid TransactionId { get; set; }
        public String TransactionDate { get; set; }
        public String EntryType { get; set; }
        public double InitialBalance { get; set; }
        public Double Amount { get; set; }
        public Double Balance { get; set; }
        public String Description { get; set; }
    }
}
