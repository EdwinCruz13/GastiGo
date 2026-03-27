using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Public.Entities
{
    /// <summary>
    /// tabla de impuestos, con los rangos de ingresos, el porcentaje a aplicar, la base a descontar y el exceso a aplicar sobre el monto que exceda el rango mínimo
    /// </summary>
    public class IncomeTax
    {
        public int Id { get; private set; }
        public Double Min { get; private set; }
        public Double Max { get; private set; }
        public Double Percentage { get; private set; }
        public Double Base { get; private set; }
        public Double Excess { get; private set; }
        private IncomeTax() { } // EF
        public IncomeTax(int id, Double min, Double max, Double percentage, Double @base, Double excess)
        {
            Id = id;
            Min = min;
            Max = max;
            Percentage = percentage;
            Base = @base;
            Excess = excess;
        }
    }

   
}
