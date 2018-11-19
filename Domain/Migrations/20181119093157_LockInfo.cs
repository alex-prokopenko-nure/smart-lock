using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class LockInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Locks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "Locks");
        }
    }
}
