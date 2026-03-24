using Domain.Common;
using Domain.Features.Finances.ValueObject;
using Domain.Features.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Transaction : AuditableEntity
    {
        public Guid TransactionId => Id;

        public Guid UserId { get; private set; } 
        public User User { get; private set; } = default!;

        public Guid TransactionTypeId { get; private set; } 
        public TransactionType TransactionType { get; private set; } = default!;

        public Guid CategoryId { get; private set; } 
        public Category Category { get; private set; } = default!;

        public String Description { get; set; }
        public Guid? TransferGroupID { get; private set; } = null;
        public DateTime TransactionDate { get; private set; }

        public String Reference { get; private set; }

        public ICollection<TransactionDetail> Details { get; private set; } = new List<TransactionDetail>();


        private Transaction() { } // EF


        public Transaction(Guid userId, Guid transactionTypeId, Guid categoryId, string description, DateTime transactionDate, string reference, Guid? transferGroupId = null)
        {
            UserId = userId;
            TransactionTypeId = transactionTypeId;
            CategoryId = categoryId;
            Description = description;
            TransactionDate = DateTime.UtcNow;  //transactionDate ?? 
            TransferGroupID = transferGroupId;
            Reference = reference;
        }


    }
}
