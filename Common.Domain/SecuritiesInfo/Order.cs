using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Order
    {
        public string OrderId { get; set; }
        public string PairId { get; set; }
        public string ExchangeId { get; set; }
        public char Type { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public long Timestamp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
