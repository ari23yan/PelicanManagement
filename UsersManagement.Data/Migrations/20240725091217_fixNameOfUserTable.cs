using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixNameOfUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_UserRoleId",
                schema: "Account",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityLog_UserActivityLogType_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivityLogType",
                schema: "Common",
                table: "UserActivityLogType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivityLog",
                schema: "Common",
                table: "UserActivityLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "Account",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationLog",
                schema: "Common",
                table: "ApplicationLog");

            migrationBuilder.RenameTable(
                name: "UserActivityLogType",
                schema: "Common",
                newName: "UserActivityLogTypes",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "UserActivityLog",
                schema: "Common",
                newName: "UserActivityLogs",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "Account",
                newName: "Users",
                newSchema: "Account");

            migrationBuilder.RenameTable(
                name: "ApplicationLog",
                schema: "Common",
                newName: "ApplicationLogs",
                newSchema: "Common");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivityLog_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLogs",
                newName: "IX_UserActivityLogs_UserActivityLogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserRoleId",
                schema: "Account",
                table: "Users",
                newName: "IX_Users_UserRoleId");

            migrationBuilder.AlterColumn<string>(
                name: "OtpCode",
                schema: "Account",
                table: "Users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                schema: "Account",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivityLogTypes",
                schema: "Common",
                table: "UserActivityLogTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivityLogs",
                schema: "Common",
                table: "UserActivityLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Account",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationLogs",
                schema: "Common",
                table: "ApplicationLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityLogs_UserActivityLogTypes_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLogs",
                column: "UserActivityLogTypeId",
                principalSchema: "Common",
                principalTable: "UserActivityLogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityLogs_UserActivityLogTypes_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                schema: "Account",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Account",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivityLogTypes",
                schema: "Common",
                table: "UserActivityLogTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivityLogs",
                schema: "Common",
                table: "UserActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationLogs",
                schema: "Common",
                table: "ApplicationLogs");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Account",
                newName: "User",
                newSchema: "Account");

            migrationBuilder.RenameTable(
                name: "UserActivityLogTypes",
                schema: "Common",
                newName: "UserActivityLogType",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "UserActivityLogs",
                schema: "Common",
                newName: "UserActivityLog",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "ApplicationLogs",
                schema: "Common",
                newName: "ApplicationLog",
                newSchema: "Common");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserRoleId",
                schema: "Account",
                table: "User",
                newName: "IX_User_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivityLogs_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLog",
                newName: "IX_UserActivityLog_UserActivityLogTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "OtpCode",
                schema: "Account",
                table: "User",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                schema: "Account",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "Account",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivityLogType",
                schema: "Common",
                table: "UserActivityLogType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivityLog",
                schema: "Common",
                table: "UserActivityLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationLog",
                schema: "Common",
                table: "ApplicationLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_UserRoleId",
                schema: "Account",
                table: "User",
                column: "UserRoleId",
                principalSchema: "Account",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityLog_UserActivityLogType_UserActivityLogTypeId",
                schema: "Common",
                table: "UserActivityLog",
                column: "UserActivityLogTypeId",
                principalSchema: "Common",
                principalTable: "UserActivityLogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
