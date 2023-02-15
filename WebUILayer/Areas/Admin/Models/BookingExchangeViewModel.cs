namespace WebUILayer.Areas.Admin.Models
{
    public class BookingExchangeViewModel
    {
        public string Base_currency { get; set; }
        public string Base_currency_date { get; set; }
        public Exchange_Rates[] Exchange_rates { get; set; }

        public class Exchange_Rates
        {
            public string Exchange_rate_buy { get; set; }
            public string Currency { get; set; }
        }
    }
}