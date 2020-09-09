using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookLanguage> BookLanguage { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Author_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Website)
                    .IsUnique()
                    .HasFilter("([Website] IS NOT NULL)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfDeath).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(168);

                entity.Property(e => e.PhotoImageType)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Website).HasMaxLength(800);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.DownloadLink)
                    .HasName("AK_Book_DownloadLink")
                    .IsUnique();

                entity.HasIndex(e => e.Isbn10)
                    .HasName("AK_Book_ISBN10")
                    .IsUnique();

                entity.HasIndex(e => e.Isbn13)
                    .HasName("AK_Book_ISBN13")
                    .IsUnique();

                entity.HasIndex(e => new { e.Title, e.Subtitle, e.AuthorId })
                    .HasName("AK_Book_TitleSubtitleAuthorId")
                    .IsUnique();

                entity.Property(e => e.CoverImageType)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DownloadLink)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.Isbn10)
                    .HasColumnName("ISBN10")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Isbn13)
                    .IsRequired()
                    .HasColumnName("ISBN13")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.PublicationDate).HasColumnType("date");

                entity.Property(e => e.Subtitle).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__AuthorId__73BA3083");

                entity.HasOne(d => d.BookLanguage)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookLanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__BookLangua__76969D2E");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__CategoryId__75A278F5");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__PublisherI__74AE54BC");
            });

            modelBuilder.Entity<BookLanguage>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("AK_BookLanguage_Code")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("AK_BookLanguage_Name")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Category_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Publisher_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
