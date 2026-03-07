using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Auth.Entities
{
    public class TwoFactorCode : AuditableEntity
    {
        public Guid TwoFactorCodeID => Id;
        public Guid UserID { get; private set; }
        public string Code { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; }
        //FK
        public int TwoFactorStatusID { get; private set; }
        public TwoFactorStatus Status { get; private set; } = default!;

        private TwoFactorCode() { } // EF

        public TwoFactorCode(Guid userId, string code, int statusID,  DateTime expiresAt)
        {
            UserID = userId;
            Code = code;
            ExpiresAt = expiresAt;
            TwoFactorStatusID = 1; // Active
        }

        public void MarkAsUsed() => TwoFactorStatusID = 2;
        public void MarkAsExpired() => TwoFactorStatusID = 3;
        public void MarkAsReplaced() => TwoFactorStatusID = 4;

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    }
}
