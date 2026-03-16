using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Auth.Entities
{

    public class TwoFactorStatus
    {
        public Int32 TwoFactorStatusId { get; private set; }
        public string Status { get; private set; }


        public TwoFactorStatus() { } //EF 
        public TwoFactorStatus(Int32 twoFactorStatusID, string status)
        {
            TwoFactorStatusId = twoFactorStatusID;
            Status = status;
        }

      
    }

    //    public enum TwoFactorStatus
    //{
    //    Active = 1,
    //    Used = 0,
    //    Expired = 2,
    //    Replaced = 3
    //}
}
