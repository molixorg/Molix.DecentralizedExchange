using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class CustomerFictiousPortfolio
    {
        public int OId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public IList<CustomerFictiousPortfolioEntryTransaction> Transactions { get; set; }
    }
}
