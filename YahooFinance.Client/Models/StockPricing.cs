using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockPricing
    {
        public decimal Ask { get; set; }

        public decimal Bid { get; set; }

        public decimal AskRealtime { get; set; }

        public decimal BidRealtime { get; set; }

        public decimal PreviousClose { get; set; }

        public decimal Open { get; set; }
    }
}
