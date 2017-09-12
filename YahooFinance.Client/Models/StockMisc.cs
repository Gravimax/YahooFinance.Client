using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockMisc
    {
        public decimal DaysRange { get; set; }

        public decimal DaysRangeRealtime { get; set; }

        public decimal DaysValueChange { get; set; }

        public decimal DaysValueChangeRealtime { get; set; }

        public string TickerTrend { get; set; }
    }
}
