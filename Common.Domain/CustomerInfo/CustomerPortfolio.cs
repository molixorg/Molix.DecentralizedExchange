using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class CustomerPortfolio
    {
        public Dictionary<string, string> Funds { get; set; }

        public int TransactionCount { get; set; }

        public int OpenOrders { get; set; }

        public long ServerTime { get; set; }

        public CustomerRights Rights { get; set; }
    }

    [Serializable]
    public class CustomerRights
    {
        public bool AccessInfo { get; set; }

        public bool CanTrade { get; set; }

        public bool CanWithdraw { get; set; }
    }
}
