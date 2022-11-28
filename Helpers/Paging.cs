using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Helpers
{
    public class PagedList<T>:List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get;  private set; }
        public  static int PageSize { get; private set; } = 10;
        public int TotalCount { get;  private set; }
        public bool hasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber=1)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * PageSize)
                .Take(PageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, PageSize);
        }
    }
}
