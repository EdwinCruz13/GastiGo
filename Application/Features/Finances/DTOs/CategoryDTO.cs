using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class CategoryDTO
    {
        //public Guid CategoryID { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid NatureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }

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

    }
}
