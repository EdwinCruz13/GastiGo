using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class TransactionDetail : AuditableEntity
    {
        public Guid TransactionDetailId => Id;
        public Guid TransactionId { get; private set; }
        public Transaction Transaction { get; private set; } = default!;
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; } = default!;
        public Double Amount { get; private set; }
        public String EntryType { get; private set; } = string.Empty;

        private TransactionDetail() { } // EF
        public TransactionDetail(Guid transactionId, Guid AccountId, double amount, String entryType)
        {
            this.TransactionId = transactionId;
            this.AccountId = AccountId;
            this.Amount = amount;
            this.EntryType = entryType;
        }
    

    }
}
