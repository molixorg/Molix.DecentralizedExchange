using System;

namespace Common.Domain
{
    [Serializable]
    public class Transaction
    {
        public string PairId { get; set; }

        public string ExchangeId { get; set; }

        public char OrderType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public long Timestamp { get; set; }
    }
}
