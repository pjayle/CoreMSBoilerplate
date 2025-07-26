using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gumfa.services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class addednordernameinordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "orders");
        }
    }
}
