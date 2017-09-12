using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockSymbolInfo
    {
        public string MoreInfo { get; set; }

        public string MarketCapitalization { get; set; }

        public string MarketCapRealtime { get; set; }

        public string FloatShares { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string Symbol { get; set; }

        public string StockExchange { get; set; }

        public string SharesOutstanding { get; set; }
    }
}
