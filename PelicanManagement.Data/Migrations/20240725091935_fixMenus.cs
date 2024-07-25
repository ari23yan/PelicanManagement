using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PelicanManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Menus_MenuId",
                schema: "Account",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_MenuId",
                schema: "Account",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenus",
                schema: "Account",
                table: "RoleMenus");

            migrationBuilder.DropIndex(
                name: "IX_RoleMenus_RoleId",
                schema: "Account",
                table: "RoleMenus");

            migrationBuilder.DropColumn(
                name: "MenuId",
                schema: "Account",
                table: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenus",
                schema: "Account",
                table: "RoleMenus",
                columns: new[] { "RoleId", "MenuId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenus",
                schema: "Account",
                table: "RoleMenus");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                schema: "Account",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenus",
                schema: "Account",
                table: "RoleMenus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_MenuId",
                schema: "Account",
                table: "Roles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                schema: "Account",
                table: "RoleMenus",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Menus_MenuId",
                schema: "Account",
                table: "Roles",
                column: "MenuId",
                principalSchema: "Account",
                principalTable: "Menus",
                principalColumn: "Id");
        }
    }
}
