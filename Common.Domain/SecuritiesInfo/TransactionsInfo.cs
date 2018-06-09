using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Domain
{
    [Serializable]
    public class TransactionsInfo
    {
        public string PairId { get; set; }

        public string ExchangeId { get; set; }

        public IList<Transaction> Transactions { get; set; }
        public TransactionsInfo(string pairId, string exchangeId, IEnumerable<Transaction> transactions)
        {
            PairId = pairId;
            ExchangeId = exchangeId;
            Transactions = transactions?.ToList();
        }
    }
}
