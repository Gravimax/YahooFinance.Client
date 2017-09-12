using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinance.Client
{
    public class PricingSection 
    {
        public PricingSection(StockPricingSection paramId, string paramName, int paramLength, string sectionSymbols)
        {
            ParamId = paramId;
            ParamName = paramName;
            ParamLength = paramLength;
            SectionSymbols = sectionSymbols;
        }

        public StockPricingSection ParamId { get; private set; }

        public string ParamName { get; private set; }

        public int ParamLength { get; private set; }

        public string SectionSymbols { get; private set; }
    }
}
