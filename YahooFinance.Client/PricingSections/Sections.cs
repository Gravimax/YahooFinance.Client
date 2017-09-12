using System.Collections.Generic;
using System.Linq;

namespace YahooFinance.Client.PricingSections
{
    public static class Sections
    {
        public static List<PricingSection> GetPricingSectionsList()
        {
            List<PricingSection> temp = new List<PricingSection>();

            temp.Add(GetPricingSection(StockPricingSection.Averages));
            temp.Add(GetPricingSection(StockPricingSection.Date));
            temp.Add(GetPricingSection(StockPricingSection.Dividends));
            temp.Add(GetPricingSection(StockPricingSection.FiftyTwoWeekPricing));
            temp.Add(GetPricingSection(StockPricingSection.Misc));
            temp.Add(GetPricingSection(StockPricingSection.Pricing));
            temp.Add(GetPricingSection(StockPricingSection.Ratios));
            temp.Add(GetPricingSection(StockPricingSection.SymbolInfo));
            temp.Add(GetPricingSection(StockPricingSection.Volume));

            return temp;
        }


        public static PricingSection GetPricingSection(StockPricingSection section)
        {
            switch (section)
            {
                case StockPricingSection.Averages:
                    return new PricingSection(StockPricingSection.Averages, "Averages", 13, "c8ghk1ll1t8m5m6m7m8m3m4");
                case StockPricingSection.Date:
                    return new PricingSection(StockPricingSection.Date, "Date", 8, "c1cc6k2p2d1d2t1");
                case StockPricingSection.Dividends:
                    return new PricingSection(StockPricingSection.Dividends, "Dividends", 4, "ydr1q");
                case StockPricingSection.FiftyTwoWeekPricing:
                    return new PricingSection(StockPricingSection.FiftyTwoWeekPricing, "52 Week Pricing", 7, "kjj5k4j6k5w");
                case StockPricingSection.Misc:
                    return new PricingSection(StockPricingSection.Misc, "Misc", 5, "w1w4mm2t7");
                case StockPricingSection.Pricing:
                    return new PricingSection(StockPricingSection.Pricing, "Pricing", 6, "abb2b3po");
                case StockPricingSection.Ratios:
                    return new PricingSection(StockPricingSection.Ratios, "Ratios", 14, "ee7e8e9b4j4p5p6rr2r5r6r7s7");
                case StockPricingSection.SymbolInfo:
                    return new PricingSection(StockPricingSection.SymbolInfo, "Symbol Info", 9, "ij1j3f6nn4sxj2");
                case StockPricingSection.Volume:
                    return new PricingSection(StockPricingSection.Volume, "Volume", 5, "va5b6k3a2");
                default:
                    return null;
            }
        }


        public static List<PricingSection> GetPricingSections(params StockPricingSection[] pricingSections)
        {
            List<PricingSection> temp = new List<PricingSection>();

            foreach (var item in pricingSections)
            {
                if (temp.FirstOrDefault(x => x.ParamId == item) != null)
                {
                    throw new System.Exception("Pricing Section already exists in list. Duplicates are not allowed.");
                }

                temp.Add(GetPricingSection(item));
            }

            return temp;
        }


        public static string GetInterval(HistoricalIntervals interval)
        {
            switch (interval)
            {
                case HistoricalIntervals.Daily:
                    return "1d";
                case HistoricalIntervals.Weekly:
                    return "5d";
                case HistoricalIntervals.Monthly:
                    return "1m";
                default:
                    return "1d";
            }
        }
    }
}
