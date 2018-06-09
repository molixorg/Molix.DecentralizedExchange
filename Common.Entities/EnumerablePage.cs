using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class EnumerablePage<T> : IEnumerablePage<T>
    {
        public IEnumerable<T> PageData { get; set; }

        public int TotalCount { get; set; }

        public int TotalPageCount {
            get
            {
                return (PageSize == 0) ? 0 : (TotalCount / PageSize);
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
