using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public interface IEnumerablePage<T>
    {
        /// <summary>
        /// Gets the data for the page.
        /// </summary>
        IEnumerable<T> PageData { get; }

        /// <summary>
        /// Total amount of data that exists.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total amount of pages with current page size.
        /// </summary>
        int TotalPageCount { get; }

        /// <summary>
        /// Page number of the collection.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Page size of the collection.
        /// </summary>
        int PageSize { get; }
    }
}
