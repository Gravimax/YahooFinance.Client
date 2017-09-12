using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockAverages
    {
        public decimal AfterHourseChangeRealtime { get; set; }

        public decimal DaysLow { get; set; }

        public decimal DaysHigh { get; set; }

        public string LastTradeRealtimeWithTime { get; set; }

        public string LastTradeWithTime { get; set; }

        public decimal LastTrade { get; set; }

        public decimal OneYearTargetPrice {get;set;}

        public decimal ChangeFrom200DayMovingAverage { get; set; }

        public decimal PercentChangeFrom200DayMovingAverage { get; set; }

        public decimal ChangeFrom50DayMovingAverage { get; set; }

        public decimal PercentChangeFrom50DayMovingAverage { get; set; }

        public decimal FiftyDayMovingAverage { get; set; }

        public decimal TwoHundredDayMovingAverage { get; set; }
    }
}
