using System;
using System.Collections.Generic;

namespace Common.Domain
{
    [Serializable]
    public class PairOrder : IDisposable
    {
        public string PairId { get; set; }

        public string ExchangeId { get; set; }

        public List<List<decimal>> Asks { get; set; }

        public List<List<decimal>> Bids { get; set; }

        public long Timestamp { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
