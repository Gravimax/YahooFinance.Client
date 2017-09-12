using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client.Models
{
    public class StockDividends
    {
        public string DividendPayDate { get; set; }

        public decimal DividendPerShare { get; set; }

        public decimal DividendYield { get; set; }

        public string ExDividendDate { get; set; }
    }
}
