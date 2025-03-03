using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class wordInfoRemoveUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordInfos_UserInfos_UserID",
                table: "WordInfos");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "WordInfos",
                newName: "UserInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_WordInfos_UserID",
                table: "WordInfos",
                newName: "IX_WordInfos_UserInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_WordInfos_UserInfos_UserInfoID",
                table: "WordInfos",
                column: "UserInfoID",
                principalTable: "UserInfos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordInfos_UserInfos_UserInfoID",
                table: "WordInfos");

            migrationBuilder.RenameColumn(
                name: "UserInfoID",
                table: "WordInfos",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WordInfos_UserInfoID",
                table: "WordInfos",
                newName: "IX_WordInfos_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_WordInfos_UserInfos_UserID",
                table: "WordInfos",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
