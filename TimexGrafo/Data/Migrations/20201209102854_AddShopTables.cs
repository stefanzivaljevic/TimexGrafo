using Microsoft.EntityFrameworkCore.Migrations;

namespace TimexGrafo.Data.Migrations
{
    public partial class AddShopTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShopGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopSubGroups_ShopGroups_ShopGroupId",
                        column: x => x.ShopGroupId,
                        principalTable: "ShopGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    FullPrice = table.Column<double>(nullable: false),
                    SubGroupId = table.Column<int>(nullable: false),
                    ItemCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_ShopSubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "ShopSubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_SubGroupId",
                table: "ShopItems",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopSubGroups_ShopGroupId",
                table: "ShopSubGroups",
                column: "ShopGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "ShopSubGroups");

            migrationBuilder.DropTable(
                name: "ShopGroups");
        }
    }
}
