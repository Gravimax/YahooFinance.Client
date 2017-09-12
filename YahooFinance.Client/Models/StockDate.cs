using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockDate
    {
        public string Change { get; set; }

        public string ChangeAndPercentChange { get; set; }

        public string ChangeInPercent { get; set; }

        public string ChangePercentRealtime { get; set; }

        public string ChangeRealtime { get; set; }

        public string LastTradeDate { get; set; }

        public string LastTradeTime { get; set; }

        public string TradeDate { get; set; }
    }
}
