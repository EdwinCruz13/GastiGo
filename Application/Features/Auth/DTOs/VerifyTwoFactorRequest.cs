using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.DTOs
{
    public class VerifyTwoFactorRequest
    {
        public Guid TwoFactorId { get; set; }
        public string Code { get; set; } = null!;
    }
}
