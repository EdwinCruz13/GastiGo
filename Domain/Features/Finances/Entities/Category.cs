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
        public Guid CategoryId => Id;

        public Guid UserId { get; private set; }
        public User User { get; private set; } = default!;

        public Guid? ParentId { get; private set; } = null;
        public Category Parent { get; private set; } = default!;

        public Guid NatureId { get; private set; }
        public Nature Nature { get; private set; } = default!;

        public String Name { get; private set; }
        public String Description { get; private set; }

        public ICollection<Category> Subcategories { get; private set; } = new List<Category>();

        public Boolean isActive { get; private set; } = false;

        public Boolean isSalary { get; set; }

        public ICollection<CategoryParams> Params { get; private set; } = new List<CategoryParams>();

        private Category() { } // EF


        /// <summary>
        /// setea el usuario, la naturaleza, el nombre y la descripción de la categoría, así como su categoría padre si es que tiene una
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="natureId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="parentId"></param>
        public Category(Guid userId, Guid natureId, string name, string description, Guid? parentId = null, Boolean isSalary = false)
        {
            UserId = userId;
            NatureId = natureId;
            Name = name;
            Description = description;
            ParentId = parentId;
            isActive = true;
            this.isSalary = isSalary;
        }

        public void Update(Guid userId, string name, string description, Guid natureId, Guid? parentId, bool isActive, Boolean isSalary = false)
        {
            Name = name;
            Description = description;
            NatureId = natureId;
            ParentId = parentId;
            this.isActive = isActive;
            this.isSalary = isSalary;
        }

        /// <summary>
        /// marca la categoría como eliminada, lo que permite mantener un historial de categorías eliminadas 
        /// sin perder la integridad de los datos relacionados con ellas.
        /// </summary>
        public void MarkAsDeleted()
        {
            isActive = false;
        }
    }
}
