using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TP.Service
{
    public interface IRealTimeAnalysisClient
    {
        Action<IList<TransactionAnalysis>> OnReceiveTransactionAnalysis { get; set; }

        bool Init();
    }
}
