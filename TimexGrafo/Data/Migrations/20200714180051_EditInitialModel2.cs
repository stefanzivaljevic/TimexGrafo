using Microsoft.EntityFrameworkCore.Migrations;

namespace TimexGrafo.Data.Migrations
{
    public partial class EditInitialModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "Items");
        }
    }
}
