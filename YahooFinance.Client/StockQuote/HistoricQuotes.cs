using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YahooFinance.Client
{
    /// <summary>
    /// Maximum of 2000 requests per hour for free api.
    /// </summary>
    /// <seealso cref="YahooFinance.Client.StockQuoteBase" />
    public class HistoricQuotes : StockQuoteBase
    {
        private const string HISTORICAL_QUOTES_BASE_URL = "https://query1.finance.yahoo.com/v7/finance/download/";

        private AccessToken token;


        /// <summary>
        /// Gets the stock history.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <param name="period1">The period1.</param>
        /// <param name="period2">The period2.</param>
        /// <param name="histInterval">The hist interval.</param>
        /// <returns></returns>
        public List<HistoricalQuote> GetStockHistory(string stockSymbol, DateTime period1, DateTime period2, HistoricalIntervals histInterval)
        {
            return RetrieveHistoricalQuotes(stockSymbol, period1, period2, histInterval);
        }


        /// <summary>
        /// Gets the stock history asynchronous.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <param name="period1">The period1.</param>
        /// <param name="period2">The period2.</param>
        /// <param name="histInterval">The hist interval.</param>
        /// <returns></returns>
        public async Task<List<HistoricalQuote>> GetStockHistoryAsync(string stockSymbol, DateTime period1, DateTime period2, HistoricalIntervals histInterval)
        {
            Task<List<HistoricalQuote>> task = Task.Factory.StartNew<List<HistoricalQuote>>(() => RetrieveHistoricalQuotes(stockSymbol, period1, period2, histInterval));

            List<HistoricalQuote> temp = await task;

            return temp;
        }


        private List<HistoricalQuote> RetrieveHistoricalQuotes(string stockSymbol, DateTime period1, DateTime period2, HistoricalIntervals histInterval)
        {
            if (token == null || string.IsNullOrEmpty(token.Crumb))
            {
                token = GetAccessToken(stockSymbol);
            }

            if (token == null)
            {
                throw new Exception("Token is invalid or unable to retrieve token");
            }

            string csvData = DownloadHistoricalData(BuildHistoricalQueryString(stockSymbol, period1, period2, histInterval));
            List<HistoricalQuote> temp = ParseHistoricalQuotes(csvData);

            return temp;
        }


        private string BuildHistoricalQueryString(string stockSymbol, DateTime period1, DateTime period2, HistoricalIntervals histInterval)
        {
            string interval = PricingSections.Sections.GetInterval(histInterval);

            string temp = "{0}?period1={1}&period2={2}&interval={3}&events=history&crumb={4}";
            string url = HISTORICAL_QUOTES_BASE_URL + string.Format(temp, stockSymbol, Math.Round(DateTimeToUnixTimestamp(period1)), Math.Round(DateTimeToUnixTimestamp(period2)), interval, token.Crumb);
            return url;
        }


        private string DownloadHistoricalData(string url)
        {
            string data;

            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.Cookie, token.Cookie);
                data = webClient.DownloadString(url);
            }

            return data;
        }


        private List<HistoricalQuote> ParseHistoricalQuotes(string csvData)
        {
            string[] rows = csvData.Replace("\r", "").Split(LINE_DELIMITER);

            List<HistoricalQuote> temp = new List<HistoricalQuote>();

            for (int i = 1; i < rows.Length; i++)
            {
                if (!string.IsNullOrEmpty(rows[i]))
                {
                    temp.Add(ParseHistoricalQuote(rows[i]));
                }
            }

            return temp;
        }

        private HistoricalQuote ParseHistoricalQuote(string csvData)
        {
            List<string> data = csvData.Split(CSV_DELIMITER).ToList();

            return new HistoricalQuote
            {
                Date = CleanString(data[0]),
                Open = ParseToDecimal(data[1]),
                High = ParseToDecimal(data[2]),
                Low = ParseToDecimal(data[3]),
                Close = ParseToDecimal(data[4]),
                AdjustedClose = ParseToDecimal(data[5]),
                Volume = CleanString(data[6])
            };
        }
    }
}
