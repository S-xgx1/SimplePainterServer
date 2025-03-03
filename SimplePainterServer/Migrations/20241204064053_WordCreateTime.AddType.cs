using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class WordCreateTimeAddType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "WordCreateTime",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "WordCreateTime");
        }
    }
}
