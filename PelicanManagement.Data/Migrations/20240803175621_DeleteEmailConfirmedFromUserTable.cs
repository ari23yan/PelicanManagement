using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PelicanManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteEmailConfirmedFromUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "Account",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "Account",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
