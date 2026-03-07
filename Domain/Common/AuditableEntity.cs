using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{

    /// <summary>
    /// permite que las entidades que heredan de esta clase tengan un Id único, una fecha de creación y una fecha de actualización
    /// da boluda estar repitiendo el mismo código en cada entidad
    /// </summary>
    public abstract class AuditableEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        //public DateTime? UpdatedAt { get; protected set; }

        protected AuditableEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
