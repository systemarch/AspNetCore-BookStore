using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Api.Models
{
    public class QueryParameters
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
