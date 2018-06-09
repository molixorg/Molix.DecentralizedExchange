using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Pair
    {
        public string PairId { get; set; }
        public string ExchangeId { get; set; }
        public string UnitId { get; set; }
        public int UnitDesc { get; set; }
        public string CurrencyId { get; set; }
        public int CurrencyDesc { get; set; }
        public bool Active { get; set; }
    }
}
