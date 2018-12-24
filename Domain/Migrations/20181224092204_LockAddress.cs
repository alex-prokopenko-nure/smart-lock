using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class LockAddress : Migration
    {
        public static string COMMIT_SQL = "UPDATE Locks SET Address = 'http://localhost:33334'";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Locks",
                nullable: true);
            migrationBuilder.Sql(COMMIT_SQL);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Locks");
        }
    }
}
