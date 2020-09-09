using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }
        public byte[] PhotoImage { get; set; }
        public string PhotoImageType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string Biography { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
