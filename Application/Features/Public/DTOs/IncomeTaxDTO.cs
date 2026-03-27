using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.DTOs
{
    public class IncomeTaxDTO
    {
        public int Id { get; set; }
        public Double Min { get; set; }
        public Double Max { get; set; }
        public Double Base { get; set; }
        public Double Percentage { get; set; }
        public Double Excess { get; set; }
    }
}
