using Microsoft.EntityFrameworkCore.Migrations;

namespace TimexGrafo.Data.Migrations
{
    public partial class EditedInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Subgroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Subgroups");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Groups");
        }
    }
}
