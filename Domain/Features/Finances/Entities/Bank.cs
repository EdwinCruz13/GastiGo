using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Bank : AuditableEntity
    {
        public Guid BankID => Id; // alias para el Id de AuditableEntity, para que sea más claro en el contexto de Bank
        public String Name { get; private set; }
        public String Abbre { get; private set; }
        public double TransferFee { get; private set; }

        private Bank() { } // EF

        public Bank(String name, String abbre, double transferFee)
        {
            Name = name;
            Abbre = abbre;
            TransferFee = transferFee;
        }

        /// <summary>
        /// edit the transfer fee of the bank
        /// </summary>
        /// <param name="newFee"></param>
        public void UpdateTransferFee(double newFee)
        {
            TransferFee = newFee;
        }
    }
}
