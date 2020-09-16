using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public partial class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string Title { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Subtitle { get; set; }

        [Required]
        [DisplayName("Author")]
        public int AuthorId { get; set; }

        [DisplayName("Publisher")]
        public int PublisherId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of publication")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(10)]
        [DisplayName("ISBN10")]
        public string Isbn10 { get; set; }

        [Required]
        [StringLength(13)]
        [DisplayName("ISBN13")]
        public string Isbn13 { get; set; }

        [DisplayName("Total pages")]
        public int TotalPages { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Maturity")]
        public bool IsMature { get; set; }

        public byte[] CoverImage { get; set; }
        public string CoverImageType { get; set; }

        [DisplayName("Language")]
        public int BookLanguageId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        [DisplayName("Download Link")]
        public string DownloadLink { get; set; }

        public virtual Author Author { get; set; }
        public virtual BookLanguage BookLanguage { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }

        public string FullTitle
        {
            get
            {
                return Title + (string.IsNullOrEmpty(Subtitle) ? "" : $": {Subtitle}"); 
            }
        }
    }
}
