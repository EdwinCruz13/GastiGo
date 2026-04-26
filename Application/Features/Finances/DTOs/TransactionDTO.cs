using Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.DTOs
{
    public class TransactionDTO
    {
        public Guid UserId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;

        //propiedades que se deben de tomar en cuenta para la insercción en detail,
        //ya que el detalle es el que tiene la información de la cuenta y el monto, además del tipo de entrada (ingreso o gasto)
        public Guid? FromAccountId { get; set; } //cuenta de origen, puede ser nula si es un ingreso
        public Guid? ToAccountId { get; set; } //cuenta de destino, puede ser nula si es un gasto
        public decimal Amount { get; set; } //monto de la transacción
        public String EntryType { get; set; } = string.Empty; //tipo de entrada, puede ser "IN" o "OUT"
        public DateTime? dateTransaction { get; set; }
    }


    /// <summary>
    /// primer movimiento de una transacción, se utiliza para la creación de una transacción
    /// </summary>
    public class TransactionMovementDTO 
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public Guid? CategoryId { get; set; } = default;
        public decimal Amount { get; set; }

        public DateTime? dateTransaction { get; set; }

    }

    public class TransactionResponseDTO
    {
        public Guid TransactionId { get; set; }
        public UserDTO User { get; set; } = default!;
        public TransactionTypeDTO TransactionType { get; set; } = default!;
        public CategoryResponseDTO? Category { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public Guid? TransferGroupId { get; set; }
        public string Reference { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal PreviousBalance { get; set; }
        public String EntryType { get; set; } = string.Empty;
        public AccountResponseDTO? Account { get; set; }

    }
}
