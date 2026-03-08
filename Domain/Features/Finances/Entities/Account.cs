using Domain.Common;
using Domain.Features.Users.Entities;


namespace Domain.Features.Finances.Entities
{
    public class Account : AuditableEntity
    {
        public Guid AccountID => Id;

        public Guid UserID { get; private set; }
        public User User { get; private set; } = default!;

        public Guid AccountTypeID { get; private set; }
        public AccountType AccountType { get; private set; } = default!;

        public Guid CurrecyID { get; private set; }
        public Currency Currecy { get; private set; } = default!;

        public Guid BankID { get; private set; }
        public Bank Bank { get; private set; } = default!;

        public String Name { get; set; }
        public String Description { get; set; }
        public Double Balance { get; private set; } 

        private Account() { } // EF

        public Account(Guid userId, Guid accountTypeId, Guid currecyId, Guid bankId, string name, string description, double balance)
        {
            UserID = userId;
            AccountTypeID = accountTypeId;
            CurrecyID = currecyId;
            BankID = bankId;
            Name = name;
            Description = description;
            Balance = balance;
        }

        /// <summary>
        /// anade una cantidad al balance de la cuenta, representando un deposito
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(double amount)
        {
            Balance += amount;
        }

        /// <summary>
        /// retira una cantidad del balance de la cuenta, representando un retiro. Si el monto a retirar es mayor que el balance disponible, se lanza una excepción para evitar sobregiros.
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }



    }
}
