namespace BLTC.Web.ViewModels.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public string Title { get; set; }

        public int PageIndex { get; private set; }

        public int TotalPages { get; set; }

        public bool PreviousPage
        {
            get { return this.PageIndex > 1; }
        }

        public bool NextPage
        {
            get { return this.PageIndex < this.TotalPages; }
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var pageList = new PaginatedList<T>(items, count, pageIndex, pageSize);

            return pageList;
        }
    }
}
