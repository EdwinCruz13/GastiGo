using Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Bank : AuditableEntity
    {
        public Guid BankId => Id; // alias para el Id de AuditableEntity, para que sea más claro en el contexto de Bank
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
        /// actualiza el nombre, la abreviatura y la tarifa de transferencia del banco, lo que permite modificar la información del banco según sea necesario.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="abre"></param>
        /// <param name="transferFee"></param>
        public void Update(string name, string abre, double transferFee)
        {
            this.Name = name;
            this.Abbre = abre;
            this.TransferFee = transferFee;
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
