using System.Collections.Generic;

namespace Ecommerce.WebAPI._Core.PagedList
{
    public class PagedQuery<T>
    {
        public PagedQuery(IEnumerable<T> data, int totalRecords)
        {
            Data = data;
            TotalRecords = totalRecords;
        }

        public IEnumerable<T> Data { get; private set; }
        public int TotalRecords { get; private set; }
    }
}
