using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBRPGFAPI.Migrations
{
    /// <inheritdoc />
    public partial class modelfixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroSpellList",
                table: "HeroSpellList");

            migrationBuilder.DropIndex(
                name: "IX_HeroSpellList_HeroId",
                table: "HeroSpellList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HeroSpellList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroSpellList",
                table: "HeroSpellList",
                columns: new[] { "HeroId", "SpellId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroSpellList",
                table: "HeroSpellList");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HeroSpellList",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroSpellList",
                table: "HeroSpellList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HeroSpellList_HeroId",
                table: "HeroSpellList",
                column: "HeroId");
        }
    }
}
