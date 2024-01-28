using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class ga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRefreshTokens_AspNetUsers_UserId",
                table: "userRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRefreshTokens",
                table: "userRefreshTokens");

            migrationBuilder.RenameTable(
                name: "userRefreshTokens",
                newName: "userRefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_userRefreshTokens_UserId",
                table: "userRefreshToken",
                newName: "IX_userRefreshToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userRefreshToken_AspNetUsers_UserId",
                table: "userRefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRefreshToken_AspNetUsers_UserId",
                table: "userRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken");

            migrationBuilder.RenameTable(
                name: "userRefreshToken",
                newName: "userRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_userRefreshToken_UserId",
                table: "userRefreshTokens",
                newName: "IX_userRefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRefreshTokens",
                table: "userRefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userRefreshTokens_AspNetUsers_UserId",
                table: "userRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
