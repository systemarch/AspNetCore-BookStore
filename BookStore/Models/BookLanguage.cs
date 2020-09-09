using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class BookLanguage
    {
        public BookLanguage()
        {
            Books = new HashSet<Book>();
        }

        public int BookLanguageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
