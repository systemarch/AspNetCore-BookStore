using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Api.Models
{
    public class AuthorsQueryParameters : QueryParameters
    {
        public string Name { get; set; } = "";
    }
}
