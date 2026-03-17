using Application.Features.Users.DTOs;


namespace Application.Features.Finances.DTOs
{
    public class AccountDTO
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid AccountTypeId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid BankId { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public Double Balance { get; set; }
    }

    public class AccountResponseDTO
    {
        public Guid AccountId { get; set; }
        public UserDTO User { get; set; } = default!;
        public AccountTypeDTO AccountType { get; set; } = default!;
        public CurrencyDTO Currecy { get; set; } = default!;
        public BankResponseDTO Bank { get; set; } = default!;

        public String Name { get; set; }
        public String Description { get; set; }
        public Double Balance { get; set; }
    }

}
