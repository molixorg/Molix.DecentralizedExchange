using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TP.Service
{
    public interface IRealTimeMarketDataClient
    {
        Action<IList<Instrument>> OnChangeInstruments { get; set; }
        Action<IList<PairOrder>> OnChangePairOrder { get; set; }
        Action<IList<PairOrder>> OnChangeDeepPairOrder { get; set; }
        Action<TransactionsInfo> OnChangeTransaction { get; set; }

        bool Init(string queueName = null);
    }
}
