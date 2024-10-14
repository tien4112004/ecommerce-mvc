using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupportForMultipleImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Products",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "Images");
        }
    }
}
