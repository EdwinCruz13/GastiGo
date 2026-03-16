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
        public Guid TwoFactorCodeId => Id;
        public Guid UserId { get; private set; }
        public string Code { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; }
        //FK
        public int TwoFactorStatusId { get; private set; }
        public TwoFactorStatus Status { get; private set; } = default!;

        private TwoFactorCode() { } // EF

        public TwoFactorCode(Guid userId, string code, int statusID,  DateTime expiresAt)
        {
            UserId = userId;
            Code = code;
            ExpiresAt = expiresAt;
            TwoFactorStatusId = 1; // Active
        }

        public void MarkAsUsed() => TwoFactorStatusId = 2;
        public void MarkAsExpired() => TwoFactorStatusId = 3;
        public void MarkAsReplaced() => TwoFactorStatusId = 4;

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    }
}
