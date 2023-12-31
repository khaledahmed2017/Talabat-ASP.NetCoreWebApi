using Microsoft.EntityFrameworkCore.Migrations;

namespace TalabatRepository.Migrations
{
    public partial class urlDrod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductItemOrder_PictureUrl",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "ProductItemOrder_ProductName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductItemOrder_ProductName",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductItemOrder_PictureUrl",
                table: "OrderItems",
                type: "int",
                nullable: true);
        }
    }
}
