using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class TransactionDetailDTO
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public Double Amount { get; set; }
        public String EntryType { get; set; } = string.Empty;

    }

    public class TransactionDetailResponseDTO
    {
        public Guid TransactionDetailId { get; set; }
        public TransactionResponseDTO Transaction { get; set; } = default!;
        public AccountResponseDTO Account { get; set; } = default!;
        public Double Amount { get; set; }
        public String EntryType { get; set; } = string.Empty;
    }
}
