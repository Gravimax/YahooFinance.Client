using System.Collections.Generic;
using YahooFinance.Client.Models;

namespace YahooFinance.Client
{
    public class StockQuote
    {
        private Stock52WeekPricing _stock52WeekPricing;
        public Stock52WeekPricing Stock52WeekPricing
        {
            get { return _stock52WeekPricing; }
            set { _stock52WeekPricing = value; }
        }

        private StockAverages _stockAverages;
        public StockAverages StockAverages
        {
            get { return _stockAverages; }
            set { _stockAverages = value; }
        }

        private StockDate _stockDate;
        public StockDate StockDate
        {
            get { return _stockDate; }
            set { _stockDate = value; }
        }

        private StockDividends _stockDividends;
        public StockDividends StockDividends
        {
            get { return _stockDividends; }
            set { _stockDividends = value; }
        }

        private StockMisc _stockMisc;
        public StockMisc StockMisc
        {
            get { return _stockMisc; }
            set { _stockMisc = value; }
        }

        private StockPricing _stockPricing;
        public StockPricing StockPricing
        {
            get { return _stockPricing; }
            set { _stockPricing = value; }
        }

        private StockRatios _stockRatios;
        public StockRatios StockRatios
        {
            get { return _stockRatios; }
            set { _stockRatios = value; }
        }

        private StockSymbolInfo _stockSymbolInfo;
        public StockSymbolInfo StockSymbolInfo
        {
            get { return _stockSymbolInfo; }
            set { _stockSymbolInfo = value; }
        }

        private StockVolume _stockVolume;
        public StockVolume StockVolume
        {
            get { return _stockVolume; }
            set { _stockVolume = value; }
        }

        private List<HistoricalQuote> _historical;
        public List<HistoricalQuote> Historical
        {
            get { return _historical; }
            set { _historical = value; }
        }
    }
}
