using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class TransactionAnalysis
    {
        public string ExchangeId { get; set; }
        public string PairId { get; set; }
        public int CollectedTimestamp { get; set; }
        public decimal TotalBuy { get; set; }
        public decimal TotalSell { get; set; }
        public decimal TotalBuyVolume { get; set; }
        public decimal TotalSellVolume { get; set; }
        public decimal BeginPrice { get; set; }
        public decimal EndPrice { get; set; }
        public int TotalBuyTrans { get; set; }
        public int TotalSellTrans { get; set; }
    }
}
