using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gumfa.services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedempnameinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmpID",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpID",
                table: "AspNetUsers");
        }
    }
}
