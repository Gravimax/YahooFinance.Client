using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockRatios
    {
        public string BookValue { get; set; }

        public string EarningsPerShare { get; set; }

        public string EBITDA { get; set; }

        public string EPSEstimateCurrentYear { get; set; }

        public string EPSEstimateNextQuarter { get; set; }

        public string EPSEstimateNextYear { get; set; }

        public string PEGRatio { get; set; }

        public string PERatio { get; set; }

        public string PERatioRealtime { get; set; }

        public string PriceBook { get; set; }

        public string PriceEPSEstimateCurrentYear { get; set; }

        public string PriceEPSEstimateNextYear { get; set; }

        public string PriceSales { get; set; }

        public string ShortRatio { get; set; }
    }
}
