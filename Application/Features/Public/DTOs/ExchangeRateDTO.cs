using Application.Features.Finances.DTOs;

namespace Application.Features.Public.DTOs
{
    public class ExchangeRateDTO
    {
        public DateTime Date { get; set; }
        public CurrencyDTO CurrencyFrom { get; set; } = new CurrencyDTO();
        public CurrencyDTO CurrencyTo { get; set; } = new CurrencyDTO();
        public decimal Value { get; set; }
    }
}
