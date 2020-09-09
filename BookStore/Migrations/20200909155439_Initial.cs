using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 168, nullable: false),
                    PhotoImage = table.Column<byte[]>(nullable: true),
                    PhotoImageType = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "date", nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Website = table.Column<string>(maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "BookLanguage",
                columns: table => new
                {
                    BookLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLanguage", x => x.BookLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Subtitle = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ISBN10 = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    ISBN13 = table.Column<string>(unicode: false, fixedLength: true, maxLength: 13, nullable: false),
                    TotalPages = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    IsMature = table.Column<bool>(nullable: false),
                    CoverImage = table.Column<byte[]>(nullable: true),
                    CoverImageType = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    BookLanguageId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    DownloadLink = table.Column<string>(maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK__Book__AuthorId__73BA3083",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Book__BookLangua__76969D2E",
                        column: x => x.BookLanguageId,
                        principalTable: "BookLanguage",
                        principalColumn: "BookLanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Book__CategoryId__75A278F5",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Book__PublisherI__74AE54BC",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "AK_Author_Name",
                table: "Author",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_Website",
                table: "Author",
                column: "Website",
                unique: true,
                filter: "([Website] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookLanguageId",
                table: "Book",
                column: "BookLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "AK_Book_DownloadLink",
                table: "Book",
                column: "DownloadLink",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Book_ISBN10",
                table: "Book",
                column: "ISBN10",
                unique: true,
                filter: "[ISBN10] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "AK_Book_ISBN13",
                table: "Book",
                column: "ISBN13",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherId",
                table: "Book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "AK_Book_TitleSubtitleAuthorId",
                table: "Book",
                columns: new[] { "Title", "Subtitle", "AuthorId" },
                unique: true,
                filter: "[Title] IS NOT NULL AND [Subtitle] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "AK_BookLanguage_Code",
                table: "BookLanguage",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_BookLanguage_Name",
                table: "BookLanguage",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Publisher_Name",
                table: "Publisher",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "BookLanguage");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
