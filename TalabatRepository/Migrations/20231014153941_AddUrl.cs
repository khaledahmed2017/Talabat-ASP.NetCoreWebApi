using Microsoft.EntityFrameworkCore.Migrations;

namespace TalabatRepository.Migrations
{
    public partial class AddUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductItemOrder_PictureUrl",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductItemOrder_PictureUrl",
                table: "OrderItems");
        }
    }
}
