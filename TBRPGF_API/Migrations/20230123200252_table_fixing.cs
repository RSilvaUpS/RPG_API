using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBRPGFAPI.Migrations
{
    /// <inheritdoc />
    public partial class tablefixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroPortrait");

            migrationBuilder.AddColumn<string>(
                name: "PortraitLink",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortraitLink",
                table: "Heroes");

            migrationBuilder.CreateTable(
                name: "HeroPortrait",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroId = table.Column<int>(type: "int", nullable: false),
                    HeroImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroPortrait", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroPortrait_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroPortrait_HeroId",
                table: "HeroPortrait",
                column: "HeroId");
        }
    }
}
