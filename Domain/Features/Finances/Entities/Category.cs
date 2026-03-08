using Domain.Common;
using Domain.Features.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Category : AuditableEntity
    {
        public Guid CategoryID => Id;

        public Guid UserID { get; private set; }
        public User User { get; private set; } = default!;

        public Guid? ParentID { get; private set; } = null;
        public Category Parent { get; private set; } = default!;

        public Guid NatureID { get; private set; } 
        public Nature Nature { get; private set; } = default!;

        public String Name { get; private set; }
        public String Description { get; private set; }

        private Category() { } // EF


        /// <summary>
        /// setea el usuario, la naturaleza, el nombre y la descripción de la categoría, así como su categoría padre si es que tiene una
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="natureId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="parentId"></param>
        public Category(Guid userId, Guid natureId, string name, string description, Guid? parentId = null)
        {
            UserID = userId;
            NatureID = natureId;
            Name = name;
            Description = description;
            ParentID = parentId;
        }
    }
}
