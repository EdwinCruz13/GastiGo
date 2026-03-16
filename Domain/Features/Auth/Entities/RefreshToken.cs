using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Auth.Entities
{
    public class RefreshToken : AuditableEntity 
    {
        public Guid RefreshTokenId => Id; // esta sera la llave primaria
        public Guid UserId { get; private set; }
        public string Token { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; }
        public bool Revoked { get; private set; }

        private RefreshToken() { } // EF

        public RefreshToken(Guid userId, string token, DateTime expiresAt)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            Revoked = false;

        }

        public void Revoke()
        {
            Revoked = true;
        }

        public bool IsActive()
        {
            return !Revoked && ExpiresAt > DateTime.UtcNow;
        }
    }
}
