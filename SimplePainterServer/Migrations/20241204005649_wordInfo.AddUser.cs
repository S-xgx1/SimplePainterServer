using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class wordInfoAddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "WordInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordInfos_UserID",
                table: "WordInfos",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_WordInfos_UserInfos_UserID",
                table: "WordInfos",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordInfos_UserInfos_UserID",
                table: "WordInfos");

            migrationBuilder.DropIndex(
                name: "IX_WordInfos_UserID",
                table: "WordInfos");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "WordInfos");
        }
    }
}
