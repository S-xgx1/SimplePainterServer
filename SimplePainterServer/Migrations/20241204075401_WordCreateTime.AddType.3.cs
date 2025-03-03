using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class WordCreateTimeAddType3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime",
                column: "WordInfoId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime",
                column: "WordInfoId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
