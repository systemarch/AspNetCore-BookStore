using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(168)]
        public string Name { get; set; }
        public byte[] PhotoImage { get; set; }
        public string PhotoImageType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Born")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Died")]
        public DateTime? DateOfDeath { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Url]
        [MaxLength(800)]
        public string Website { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
