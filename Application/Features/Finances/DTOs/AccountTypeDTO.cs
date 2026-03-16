using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class AccountTypeDTO
    {
        public Guid AccountTypeId { get; set; }
        public String Name { get; set; }
        public String Abbre { get; set; }
    }
}
