using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YahooFinance.Client.Models;

namespace YahooFinance.Client
{
    /// <summary>
    /// Maximum of 2000 requests per hour for free api.
    /// </summary>
    /// <seealso cref="YahooFinance.Client.StockQuoteBase" />
    public class StockQuotes : StockQuoteBase
    {
        private const string QUOTES_URL_BASE = "http://finance.yahoo.com/d/quotes.csv?";


        /// <summary>
        /// Gets the stock quote.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <param name="pricingSections">The pricing sections.</param>
        /// <returns></returns>
        public StockQuote GetStockQuote(string stockSymbol, params StockPricingSection[] pricingSections)
        {
            return RetrieveStockQuote(stockSymbol, pricingSections);
        }


        /// <summary>
        /// Gets the stock quote asynchronous.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <param name="pricingSections">The pricing sections.</param>
        /// <returns></returns>
        public async Task<StockQuote> GetStockQuoteAsync(string stockSymbol, params StockPricingSection[] pricingSections)
        {
            Task<StockQuote> task = Task.Factory.StartNew<StockQuote>(() => RetrieveStockQuote(stockSymbol, pricingSections));

            StockQuote temp = await task;

            return temp;
        }


        private StockQuote RetrieveStockQuote(string stockSymbol, params StockPricingSection[] pricingSections)
        {
            List<PricingSection> ps = PricingSections.Sections.GetPricingSections(pricingSections);
            string csvData = DownloadData(BuildQueryString(stockSymbol, ps));
            StockQuote model = ParseStockQuote(csvData, ps);

            return model;
        }


        /// <summary>
        /// Gets the stock quotes.
        /// </summary>
        /// <param name="stockSymbols">The stock symbols.</param>
        /// <param name="pricingSections">The pricing sections.</param>
        /// <returns></returns>
        public List<StockQuote> GetStockQuotes(string stockSymbols, params StockPricingSection[] pricingSections)
        {
            return RetrieveStockQuotes(stockSymbols, pricingSections);
        }


        /// <summary>
        /// Gets the stock quotes asynchronous.
        /// </summary>
        /// <param name="stockSymbols">The stock symbols.</param>
        /// <param name="pricingSections">The pricing sections.</param>
        /// <returns></returns>
        public async Task<List<StockQuote>> GetStockQuotesAsync(string stockSymbols, params StockPricingSection[] pricingSections)
        {
            Task<List<StockQuote>> task = Task.Factory.StartNew<List<StockQuote>>(() => RetrieveStockQuotes(stockSymbols, pricingSections));

            List<StockQuote> temp = await task;

            return temp;
        }


        private List<StockQuote> RetrieveStockQuotes(string stockSymbols, params StockPricingSection[] pricingSections)
        {
            List<PricingSection> ps = PricingSections.Sections.GetPricingSections(pricingSections);
            string csvData = DownloadData(BuildQueryString(stockSymbols, ps));
            List<StockQuote> models = ParseStockQuotes(csvData, ps);

            return models;
        }


        private string BuildQueryString(string stockSymbols, List<PricingSection> pricingSections)
        {
            string[] temp = stockSymbols.Split(',');
            string symbols = "s=" + string.Join("+", temp) + "&";

            string sections = "f=";
            foreach (var item in pricingSections)
            {
                sections += item.SectionSymbols;
            }

            return QUOTES_URL_BASE + symbols + sections;
        }


        private string DownloadData(string url)
        {
            string data;

            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadString(url);
            }

            return data;
        }


        private List<StockQuote> ParseStockQuotes(string csvData, List<PricingSection> pricingSections)
        {
            string[] rows = csvData.Replace("\r", "").Split(LINE_DELIMITER);

            List<StockQuote> temp = new List<StockQuote>();

            foreach (var row in rows)
            {
                temp.Add(ParseStockQuote(row, pricingSections));
            }

            return temp;
        }


        private StockQuote ParseStockQuote(string csvData, List<PricingSection> pricingSections)
        {
            StockQuote model = new StockQuote();

            List<string> data = csvData.Split(CSV_DELIMITER).ToList();

            int index = 0;

            foreach (var section in pricingSections)
            {
                switch (section.ParamId)
                {
                    case StockPricingSection.FiftyTwoWeekPricing:
                        model.Stock52WeekPricing = Parse52WeekPricing(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Averages:
                        model.StockAverages = ParseAverages(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Date:
                        model.StockDate = ParseDates(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Dividends:
                        model.StockDividends = ParseDividends(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Misc:
                        model.StockMisc = ParseMisc(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Pricing:
                        model.StockPricing = ParsePricing(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Ratios:
                        model.StockRatios = ParseRatios(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.SymbolInfo:
                        model.StockSymbolInfo = ParseSymbolInfo(SpliceArray(data, index, section.ParamLength));
                        break;
                    case StockPricingSection.Volume:
                        model.StockVolume = ParseVolume(SpliceArray(data, index, section.ParamLength));
                        break;
                }

                index += section.ParamLength;
            }

            return model;
        }


        private Stock52WeekPricing Parse52WeekPricing(string[] data)
        {
            Stock52WeekPricing temp = new Stock52WeekPricing();

            temp.FiftyTwoWeekHigh = ParseToDecimal(data[0]);
            temp.FiftyTwoWeekLow = ParseToDecimal(data[1]);
            temp.ChangeFrom52WeelLow = ParseToDecimal(data[2]);
            temp.ChangeFrom52WeekHigh = ParseToDecimal(data[3]);
            temp.PercentChangeFrom52WeekLow = ParseToDecimal(data[4]);
            temp.PercentChangeFrom52WeekHigh = ParseToDecimal(data[5]);
            temp.FiftyTwoWeekRange = ParseToDecimal(data[6]);

            return temp;
        }


        private StockAverages ParseAverages(string[] data)
        {
            StockAverages temp = new StockAverages();

            temp.AfterHourseChangeRealtime = ParseToDecimal(data[0]);
            temp.DaysLow = ParseToDecimal(data[1]);
            temp.DaysHigh = ParseToDecimal(data[2]);
            temp.LastTradeRealtimeWithTime = CleanString(data[3]);
            temp.LastTradeWithTime = CleanString(data[4]);
            temp.LastTrade = ParseToDecimal(data[5]);
            temp.OneYearTargetPrice = ParseToDecimal(data[6]);
            temp.ChangeFrom200DayMovingAverage = ParseToDecimal(data[7]);
            temp.PercentChangeFrom200DayMovingAverage = ParseToDecimal(data[8]);
            temp.ChangeFrom50DayMovingAverage = ParseToDecimal(data[9]);
            temp.PercentChangeFrom50DayMovingAverage = ParseToDecimal(data[10]);
            temp.FiftyDayMovingAverage = ParseToDecimal(data[11]);
            temp.TwoHundredDayMovingAverage = ParseToDecimal(data[12]);

            return temp;
        }


        private StockDate ParseDates(string[] data)
        {
            StockDate temp = new StockDate();

            temp.Change = CleanString(data[0]);
            temp.ChangeAndPercentChange = CleanString(data[1]);
            temp.ChangeRealtime = CleanString(data[2]);
            temp.ChangePercentRealtime = CleanString(data[3]);
            temp.ChangeInPercent = CleanString(data[4]);
            temp.LastTradeDate = CleanString(data[5]);
            temp.TradeDate = CleanString(data[6]);
            temp.LastTradeTime = CleanString(data[7]);

            return temp;
        }


        private StockDividends ParseDividends(string[] data)
        {
            StockDividends temp = new StockDividends();

            temp.DividendPerShare = ParseToDecimal(data[0]);
            temp.ExDividendDate = CleanString(data[1]);
            temp.DividendPayDate = CleanString(data[2]);
            temp.DividendYield = ParseToDecimal(data[3]);

            return temp;
        }


        private StockMisc ParseMisc(string[] data)
        {
            StockMisc temp = new StockMisc();

            temp.DaysValueChange = ParseToDecimal(data[0]);
            temp.DaysValueChangeRealtime = ParseToDecimal(data[1]);
            temp.DaysRange = ParseToDecimal(data[2]);
            temp.DaysRangeRealtime = ParseToDecimal(data[3]);
            temp.TickerTrend = CleanString(data[4]);

            return temp;
        }


        private StockPricing ParsePricing(string[] data)
        {
            StockPricing temp = new StockPricing();

            temp.Ask = ParseToDecimal(data[0]);
            temp.Bid = ParseToDecimal(data[1]);
            temp.AskRealtime = ParseToDecimal(data[2]);
            temp.BidRealtime = ParseToDecimal(data[3]);
            temp.PreviousClose = ParseToDecimal(data[4]);
            temp.Open = ParseToDecimal(data[5]);


            return temp;
        }


        private StockRatios ParseRatios(string[] data)
        {
            StockRatios temp = new StockRatios();

            temp.EarningsPerShare = CleanString(data[0]);
            temp.EPSEstimateCurrentYear = CleanString(data[1]);
            temp.EPSEstimateNextYear = CleanString(data[2]);
            temp.EPSEstimateNextQuarter = CleanString(data[3]);
            temp.BookValue = CleanString(data[4]);
            temp.EBITDA = CleanString(data[5]);
            temp.PriceSales = CleanString(data[6]);
            temp.PriceBook = CleanString(data[7]);
            temp.PERatio = CleanString(data[8]);
            temp.PERatioRealtime = CleanString(data[9]);
            temp.PEGRatio = CleanString(data[10]);
            temp.PriceEPSEstimateCurrentYear = CleanString(data[11]);
            temp.PriceEPSEstimateNextYear = CleanString(data[12]);
            temp.ShortRatio = CleanString(data[13]);

            return temp;
        }


        private StockSymbolInfo ParseSymbolInfo(string[] data)
        {
            StockSymbolInfo temp = new StockSymbolInfo();

            temp.MoreInfo = CleanString(data[0]);
            temp.MarketCapitalization = data[1];
            temp.MarketCapRealtime = data[2];
            temp.FloatShares = data[3];
            temp.Name = CleanString(data[4]);
            temp.Notes = CleanString(data[5]);
            temp.Symbol = CleanString(data[6]);
            temp.StockExchange = CleanString(data[7]);
            temp.SharesOutstanding = data[8];

            return temp;
        }


        private StockVolume ParseVolume(string[] data)
        {
            StockVolume temp = new StockVolume();

            temp.Volume = ParseToDecimal(data[0]);
            temp.AskSize = ParseToDecimal(data[1]);
            temp.BidSize = ParseToDecimal(data[2]);
            temp.LastTradeSize = ParseToDecimal(data[3]);
            temp.AverageDailyVolume = ParseToDecimal(data[4]);

            return temp;
        }
    }
}
