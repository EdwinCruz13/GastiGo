using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    /// <summary>
    /// clase que representa un DTO (Data Transfer Object) para la creación o actualización de una categoría en el contexto de finanzas.
    /// </summary>
    public class CategoryDTO
    {
        //public Guid CategoryID { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid NatureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean isActive { get; set; }

}



    /// <summary>
    /// clase que representa un DTO (Data Transfer Object) para la respuesta de una categoría en el contexto de finanzas, 
    /// incluyendo información sobre su jerarquía, naturaleza y detalles descriptivos.
    /// </summary>
    public class CategoryResponseDTO
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
        public List<CategoryResponseDTO> Children { get; set; } = new();
        public NatureDTO Nature { get; set; } = new();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Boolean isActive { get; set; }
    }
}
