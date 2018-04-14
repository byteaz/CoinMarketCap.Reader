using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoinMarketCap.Reader.Model
{
    class Coin
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public double price_usd { get; set; }
        public double price_btc { get; set; }
        public double volume_usd_24h { get; set; }
        public double market_cap_usd { get; set; }
        public double available_supply { get; set; }
        public double total_supply { get; set; }
        public double percent_change_1h { get; set; }
        public double percent_change_24h { get; set; }
        public double percent_change_7d { get; set; }
        public double last_updated { get; set; }
    }
}
