using Application.Features.Users.DTOs;


namespace Application.Features.Finances.DTOs
{
    public class AccountDTO
    {
        public Guid AccountID { get; set; }
        public Guid UserID { get; set; }
        public Guid AccountTypeID { get; set; }
        public Guid CurrencyID { get; set; }
        public Guid BankID { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public Double Balance { get; set; }
    }

    public class AccountResponseDTO
    {
        public Guid AccountID { get; set; }
        public UserDTO User { get; set; } = default!;
        public AccountTypeDTO AccountType { get; set; } = default!;
        public CurrencyDTO Currecy { get; set; } = default!;
        public BankDTO Bank { get; set; } = default!;

        public String Name { get; set; }
        public String Description { get; set; }
        public Double Balance { get; set; }
    }

}
