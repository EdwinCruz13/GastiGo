
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;

namespace Domain.Features.Finances.Entities
{
    public class TransactionType : AuditableEntity
    {
        public Guid TransactionTypeID => Id;
        public String Name { get; private set; }
        public String Code { get; private set; }
        public int CurrentValue { get; private set; }


        private TransactionType() { } // EF

        public TransactionType(String name, String code)
        {
            Name = name;
            Code = code;
            CurrentValue = 0;
        }

        public int Next()
        {
            CurrentValue++;
            return CurrentValue;
        }

        public void UpdateCode(string newCode)
        {
            Code = newCode;
        }
    }
}
