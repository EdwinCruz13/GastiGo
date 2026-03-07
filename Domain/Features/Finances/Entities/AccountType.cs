using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class AccountType : AuditableEntity
    {
        public Guid AccountTypeID => Id;
        public String Name { get; private set; }
        public String Abbre { get; private set; }

        private AccountType() { } // EF

        public AccountType(String name, string abbre)
        {
            Name = name;
            Abbre = abbre;
        }
    }
}
