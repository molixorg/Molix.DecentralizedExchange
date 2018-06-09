using System;

namespace Common.Domain
{
    [Serializable]
    public class Instrument : IDisposable
    {
        public string PairId { get; set; }

        public string ExchangeId { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Avg { get; set; }

        public decimal Volume { get; set; }

        public decimal VolumeCurrency { get; set; }

        public decimal Last { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercent { get; set; }

        public decimal Buy { get; set; }

        public decimal Sell { get; set; }

        public long Updated { get; set; }

        public bool Active { get; set; }

        public Pair PairInformation { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
