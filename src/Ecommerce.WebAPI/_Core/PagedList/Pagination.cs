using System.Linq;

namespace Ecommerce.WebAPI._Core.PagedList
{
    public class Pagination
    {
        private readonly int pageNumberDefault = 1;
        private readonly int pageSizeDefault = 25;
        private readonly string[] prevent = new string[] { "from", "column", "database", "delete", "update" };

        public Pagination(RequestParameters parameters)
        {
            PageNumber = parameters.PageNumber < pageNumberDefault ? pageNumberDefault : parameters.PageNumber;
            PageSize = parameters.PageSize < pageSizeDefault ? pageSizeDefault : parameters.PageSize;
            OrderBy = prevent.Contains(parameters.OrderBy?.ToLower()) ? string.Empty : parameters.OrderBy;
        }

        public Pagination(int pageNumber, int pageSize, string orderBy)
        {
            PageNumber = pageNumber < pageNumberDefault ? pageNumberDefault : pageNumber;
            PageSize = pageSize < pageSizeDefault ? pageSizeDefault : pageSize;
            OrderBy = prevent.Contains(orderBy?.ToLower()) ? string.Empty : orderBy;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string OrderBy { get; set; }
    }
}
