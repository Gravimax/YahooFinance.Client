using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockVolume
    {
        public decimal AskSize { get; set; }

        public decimal AverageDailyVolume { get; set; }

        public decimal BidSize { get; set; }

        public decimal LastTradeSize { get; set; }

        public decimal Volume { get; set; }
    }
}
