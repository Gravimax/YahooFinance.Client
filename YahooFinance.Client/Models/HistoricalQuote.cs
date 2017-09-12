using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client
{
    public class HistoricalQuote
    {
        public decimal AdjustedClose { get; set; }

        public decimal Close { get; set; }

        public string Date { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Open { get; set; }

        public string Volume { get; set; }
    }
}
