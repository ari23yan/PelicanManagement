using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                schema: "Account",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                schema: "Account",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserRoleId",
                schema: "Account",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "Account",
                table: "Users",
                column: "RoleId",
                principalSchema: "Account",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "Account",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "Account",
                table: "Users",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                schema: "Account",
                table: "Users",
                newName: "IX_Users_UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                schema: "Account",
                table: "Users",
                column: "UserRoleId",
                principalSchema: "Account",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
