using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class Stock52WeekPricing
    {
        public decimal ChangeFrom52WeekHigh { get; set; }

        public decimal ChangeFrom52WeelLow { get; set; }

        public decimal FiftyTwoWeekHigh { get; set; }

        public decimal FiftyTwoWeekLow { get; set; }

        public decimal FiftyTwoWeekRange { get; set; }

        public decimal PercentChangeFrom52WeekHigh { get; set; }

        public decimal PercentChangeFrom52WeekLow { get; set; }
    }
}
