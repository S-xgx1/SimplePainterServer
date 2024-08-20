using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class WordInfoPartSpeech : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartSpeech",
                table: "WordInfos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartSpeech",
                table: "WordInfos");
        }
    }
}
