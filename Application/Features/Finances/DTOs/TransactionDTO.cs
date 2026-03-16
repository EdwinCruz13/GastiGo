using Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class TransactionDTO
    {
        public Guid UserId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string Reference { get; set; } = string.Empty;
        public Guid? TransferGroupId { get; set; }
    }

    public class TransactionResponseDTO
    {
        public Guid TransactionID { get; set; }
        public UserDTO User { get; set; } = default!;
        public TransactionTypeDTO TransactionType { get; set; } = default!;
        public CategoryResponseDTO Category { get; set; } = default!;
        public AccountResponseDTO Account { get; set; } = default!;
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string Reference { get; set; } = string.Empty;
    }
}
