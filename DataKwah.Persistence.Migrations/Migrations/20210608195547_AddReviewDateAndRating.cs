using Microsoft.EntityFrameworkCore.Migrations;

namespace DataKwah.Persistence.Migrations.Migrations
{
    public partial class AddReviewDateAndRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileName",
                table: "Reviews",
                newName: "Date");

            migrationBuilder.AddColumn<byte>(
                name: "Rating",
                table: "Reviews",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reviews",
                newName: "ProfileName");
        }
    }
}
