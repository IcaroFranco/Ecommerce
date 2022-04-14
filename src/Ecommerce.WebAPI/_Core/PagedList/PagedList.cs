using System;
using System.Collections.Generic;

namespace Ecommerce.WebAPI._Core.PagedList
{
    public class PagedList<T>
    {
        public Meta MetaData { get; private set; }
        public IEnumerable<T> Data { get; private set; }

        public PagedList(IEnumerable<T> data, Pagination paginacao, int totalDeRegistros)
        {
            double totalDePaginas = totalDeRegistros / paginacao.PageSize;
            MetaData = new()
            {
                TotalCount = totalDeRegistros,
                PageSize = paginacao.PageSize,
                CurrentPage = paginacao.PageNumber,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalDePaginas))
            };

            Data = data;
        }

        public partial class Meta
        {
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public bool HasPrevious => CurrentPage > 1;
            public bool HasNext => CurrentPage < TotalPages;
        }
    }
}
