using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Book_BookId",
                table: "Borrow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Books_BookId",
                table: "Borrow",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Books_BookId",
                table: "Borrow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Book_BookId",
                table: "Borrow",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
