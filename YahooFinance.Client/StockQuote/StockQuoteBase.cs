using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace YahooFinance.Client
{
    public class StockQuoteBase
    {
        public const char CSV_DELIMITER = ',';
        public const char LINE_DELIMITER = '\n';

        private const string TOKEN_URL_BASE = "https://finance.yahoo.com/quote/{0}?p={0}";


        public double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }


        public static DateTime UnixTimestampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
        }



        public string[] SpliceArray(List<string> data, int skip, int take)
        {
            return data.Skip(skip).Take(take).ToArray();
        }


        public decimal ParseToDecimal(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Equals("N/A")) { return 0; }

            decimal value = 0;

            decimal.TryParse(data, out value);

            return value;
        }


        public string CleanString(string data)
        {
            string temp = data.Replace("\"", "");
            temp = temp.Replace("\n", "");
            temp = temp.Replace("<b>", "");
            temp = temp.Replace("</b>", "");

            return temp;
        }


        // https://github.com/dennislwy/YahooFinanceAPI
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns></returns>
        public AccessToken GetAccessToken(string stockSymbol = "SPY")
        {
            string url = string.Format(TOKEN_URL_BASE, stockSymbol);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.CookieContainer = new CookieContainer();
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string cookie = response.GetResponseHeader("Set-Cookie").Split(';')[0];

                string html = string.Empty;

                using (Stream stream = response.GetResponseStream())
                {
                    html = new StreamReader(stream).ReadToEnd();
                }

                if (html.Length < 5000)
                {
                    return null;
                }

                string crumb = GetCrumb(html);

                if (crumb != null)
                {
                    return new AccessToken { Cookie = cookie, Crumb = crumb };
                }

                return null;
            }
        }

        private string GetCrumb(string html)
        {
            string crumb = null;

            Regex regex = new Regex("CrumbStore\":{\"crumb\":\"(?<crumb>.+?)\"}", RegexOptions.CultureInvariant | RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(html);

            if (matches.Count > 0)
            {
                crumb = matches[0].Groups["crumb"].Value;
                if (crumb.Length != 11)
                {
                    crumb = crumb.Replace("\\u002F", "/");
                }

                return crumb;
            }

            return crumb;
        }
    }
}
