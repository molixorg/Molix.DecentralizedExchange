using System;
using System.Collections;

namespace Common.Domain
{
    [Serializable]
    public class CustomerFictiousPortfolioEntryTransaction
    {
        public int PortfolioOId { get; set; }
        public string Key { get; set; }
        public char BuySell { get; set; }
        public decimal Volume { get; set; }
        public decimal Limit { get; set; }
        public decimal Commission { get; set; }
    }
}
