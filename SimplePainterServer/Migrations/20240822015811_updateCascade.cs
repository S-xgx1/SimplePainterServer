using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePainterServer.Migrations
{
    /// <inheritdoc />
    public partial class updateCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guesses_ImageInfos_ImageId",
                table: "Guesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Guesses_UserInfos_UserID",
                table: "Guesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_UserInfos_UserID",
                table: "ImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_WordInfos_WordId",
                table: "ImageInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_Guesses_ImageInfos_ImageId",
                table: "Guesses",
                column: "ImageId",
                principalTable: "ImageInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guesses_UserInfos_UserID",
                table: "Guesses",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_UserInfos_UserID",
                table: "ImageInfos",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_WordInfos_WordId",
                table: "ImageInfos",
                column: "WordId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guesses_ImageInfos_ImageId",
                table: "Guesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Guesses_UserInfos_UserID",
                table: "Guesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_UserInfos_UserID",
                table: "ImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_WordInfos_WordId",
                table: "ImageInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_Guesses_ImageInfos_ImageId",
                table: "Guesses",
                column: "ImageId",
                principalTable: "ImageInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guesses_UserInfos_UserID",
                table: "Guesses",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_UserInfos_UserID",
                table: "ImageInfos",
                column: "UserID",
                principalTable: "UserInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_WordInfos_WordId",
                table: "ImageInfos",
                column: "WordId",
                principalTable: "WordInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
