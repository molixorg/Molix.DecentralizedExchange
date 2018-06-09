using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class MarketSummary
    {
        public decimal TotalMarketCap { get; set; }
        public decimal Total24hVolume { get; set; }
        public float BitcoinPercentOfMarketCap { get; set; }
        public int ActiveCurrencies { get; set; }
        public int ActiveAssets { get; set; }
        public int ActiveMarkets { get; set; }
        public long CollectedTimestamp { get; set; }
    }
}
