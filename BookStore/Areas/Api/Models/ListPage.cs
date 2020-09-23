using BookStore.Infrastructure;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Api.Models
{
    public class ListPage<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<ListPage<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var paginatedList = await PaginatedList<T>.CreateAsync(
                source, pageIndex, pageSize);
            var listPage = new ListPage<T>()
            {
                PageIndex = paginatedList.PageIndex,
                TotalPages = paginatedList.TotalPages,
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.ToList()
            };
            return listPage;
        }
    }
}
