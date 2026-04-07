using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    /// <summary>
    /// clase para crear un banco, 
    /// con las propiedades necesarias para su creación, 
    /// sin incluir el Id, ya que este se genera automáticamente al crear el banco en la base de datos.
    /// </summary>
    public class BankDTO
    {
        public String Name { get; set; }
        public String Abbre { get; set; }
        public double TransferFee { get; set; }
        public String imgURL { get; set; }
    }

    /// <summary>
    /// clase para retornar un banco, con las propiedades necesarias para su identificación y visualización,
    /// </summary>
    public class BankResponseDTO
    {
        public Guid BankId { get; set; }
        public String Name { get; set; } = string.Empty;
        public String Abbre { get; set; } = string.Empty;
        public double TransferFee { get; set; }
        public String imgURL { get; set; } = string.Empty;
    }
}
