using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class InstrumentSummary
    {
        public string InstrumentId { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal PriceBtc { get; set; }
        public decimal Total24hVolume { get; set; }
        public decimal TotalMarketCap { get; set; }
        public decimal AvailableSupply { get; set; }
        public decimal TotalSupply { get; set; }
        public float PercentChange1h { get; set; }
        public float PercentChange24h { get; set; }
        public float PercentChange7d { get; set; }
        public long LastUpdated { get; set; }
    }
}
