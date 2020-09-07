using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokedex.Domain.Paginable
{
    public interface IPageList<T> : IList<T>
    {
        int Page { get; }
        int PageSize { get; }
        int Total { get; }
        int TotalPages { get; }
    }

    public class PageList<T> : List<T>, IPageList<T>
    {
        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int Total { get; private set; }

        public int TotalPages { get; private set; }

        public PageList(IList<T> data, int page, int size)
        {
            TotalPages = data.Count/ size;

            if (data.Count % size > 0)
                TotalPages++;

            Total = data.Count;
            Page = page;
            PageSize = size;
            AddRange(data.Skip((page - 1) * size).Take(size).ToList());
        }

    }
}

