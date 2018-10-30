using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: true),
                    Locked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LockOperations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LockOperations_Locks_LockId",
                        column: x => x.LockId,
                        principalTable: "Locks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LockRents",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LockId = table.Column<int>(nullable: false),
                    RentStart = table.Column<DateTime>(nullable: false),
                    RentFinish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockRents", x => new { x.LockId, x.UserId, x.RentStart });
                    table.ForeignKey(
                        name: "FK_LockRents_Locks_LockId",
                        column: x => x.LockId,
                        principalTable: "Locks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LockRents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LockOperations_LockId",
                table: "LockOperations",
                column: "LockId");

            migrationBuilder.CreateIndex(
                name: "IX_LockRents_UserId",
                table: "LockRents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LockOperations");

            migrationBuilder.DropTable(
                name: "LockRents");

            migrationBuilder.DropTable(
                name: "Locks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
