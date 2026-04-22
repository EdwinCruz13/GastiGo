using Domain.Common;
using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Public
{
    public class ExchangeRate : AuditableEntity
    {
        public Guid ExchangeId => Id;
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public Guid CurrencyFromId { get; set; }
        public Currency CurrencyFrom { get; set; } = default!;
        public Guid CurrencyToId { get; set; }
        public Currency CurrencyTo { get; set; } = default!;

        private ExchangeRate() { } // EF

        /// <summary>
        /// para guardar el valor del tipo de cambio en una fecha determinada
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        public ExchangeRate(DateTime date, decimal value, Guid currencyFromId, Guid currencyToId)
        {
            if (value <= 0)
                throw new ArgumentException("El tipo de cambio debe ser mayor que cero.");

            if (currencyFromId == currencyToId)
                throw new ArgumentException("Las monedas no pueden ser iguales.");

            Date = date;
            Value = value;
            CurrencyFromId = currencyFromId;
            CurrencyToId = currencyToId;
        }
    }
}
