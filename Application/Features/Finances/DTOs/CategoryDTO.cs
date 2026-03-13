using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryID { get; set; }
        public Guid UserID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid NatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }

    public class CategoryResponseDTO
    {
        public Guid CategoryID { get; set; }
        public Guid UserID { get; set; }
        public Guid? ParentID { get; set; }
        public List<CategoryResponseDTO> Children { get; set; } = new();
        public Guid NatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
