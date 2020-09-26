using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Api.Models
{
    public class BooksQueryParameters : QueryParameters
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
    }
}
