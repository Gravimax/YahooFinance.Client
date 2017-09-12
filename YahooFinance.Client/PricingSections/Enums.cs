using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client
{
    public enum StockPricingSection
    {
        Averages,
        Date,
        Dividends,
        FiftyTwoWeekPricing,
        Misc,
        Pricing,
        Ratios,
        SymbolInfo,
        Volume
    }

    public enum HistoricalIntervals
    {
        Daily,
        Weekly,
        Monthly
    }
}
