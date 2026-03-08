using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.DTOs
{
    public class UserDTO
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
