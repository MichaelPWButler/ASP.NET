namespace Dividens_Tracker.Models
{
    public class Instrument
    {
        public string? Ticker { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PnL { get; set; }
    }
}
