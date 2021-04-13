using Microsoft.EntityFrameworkCore.Migrations;

namespace TimexGrafo.Data.Migrations
{
    public partial class Seed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Subgroups_SubGroupId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "SubGroupId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Subgroups_SubGroupId",
                table: "Items",
                column: "SubGroupId",
                principalTable: "Subgroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Subgroups_SubGroupId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "SubGroupId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Subgroups_SubGroupId",
                table: "Items",
                column: "SubGroupId",
                principalTable: "Subgroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
