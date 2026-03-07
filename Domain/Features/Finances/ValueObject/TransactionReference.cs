using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.ValueObject
{

    /// <summary>
    /// este value object representa una referencia
    /// </summary>
    public class TransactionReference
    {
        public string Value { get; private set; }

        private TransactionReference() { }

        private TransactionReference(string value)
        {
            Value = value;
        }

        public static TransactionReference Create(string code, int sequence)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Transaction code cannot be empty");

            if (sequence <= 0)
                throw new ArgumentException("Sequence must be greater than zero");

            var reference = $"{code}-{sequence:D6}";

            return new TransactionReference(reference);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
