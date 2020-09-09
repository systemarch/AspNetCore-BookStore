using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        public int TotalPages { get; set; }
        public int CategoryId { get; set; }
        public bool IsMature { get; set; }
        public byte[] CoverImage { get; set; }
        public string CoverImageType { get; set; }
        public int BookLanguageId { get; set; }
        public decimal Price { get; set; }
        public string DownloadLink { get; set; }

        public virtual Author Author { get; set; }
        public virtual BookLanguage BookLanguage { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
