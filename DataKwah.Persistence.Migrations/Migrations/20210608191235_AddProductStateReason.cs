using Microsoft.EntityFrameworkCore.Migrations;

namespace DataKwah.Persistence.Migrations.Migrations
{
    public partial class AddProductStateReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ProductStates",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ProductStates");
        }
    }
}
