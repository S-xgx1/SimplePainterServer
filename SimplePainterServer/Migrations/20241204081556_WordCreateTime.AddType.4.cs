using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class WordCreateTimeAddType4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordCreateTime",
                table: "WordCreateTime");

            migrationBuilder.RenameTable(
                name: "WordCreateTime",
                newName: "WordCreateTimes");

            migrationBuilder.RenameIndex(
                name: "IX_WordCreateTime_WordInfoId",
                table: "WordCreateTimes",
                newName: "IX_WordCreateTimes_WordInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordCreateTimes",
                table: "WordCreateTimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordCreateTimes_WordInfos_WordInfoId",
                table: "WordCreateTimes",
                column: "WordInfoId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordCreateTimes_WordInfos_WordInfoId",
                table: "WordCreateTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordCreateTimes",
                table: "WordCreateTimes");

            migrationBuilder.RenameTable(
                name: "WordCreateTimes",
                newName: "WordCreateTime");

            migrationBuilder.RenameIndex(
                name: "IX_WordCreateTimes_WordInfoId",
                table: "WordCreateTime",
                newName: "IX_WordCreateTime_WordInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordCreateTime",
                table: "WordCreateTime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordCreateTime_WordInfos_WordInfoId",
                table: "WordCreateTime",
                column: "WordInfoId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
