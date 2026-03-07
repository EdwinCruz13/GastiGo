using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Currency : AuditableEntity
    {
        public Guid CurrencyID => Id;
        public String Name { get; private set; }
        public String Code { get; private set; }
        public string Symbol { get; private set; }

        private Currency() { } // EF

        public Currency(String name, String code, string symbol)
        {
            Name = name;
            Code = code;
            Symbol = symbol;
        }
    }
}
