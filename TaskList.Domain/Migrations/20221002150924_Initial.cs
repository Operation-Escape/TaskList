using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskList.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    State = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DateTimeCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    OrginalEstimate = table.Column<decimal>(type: "numeric", nullable: true, defaultValue: 0m),
                    RemainingWork = table.Column<decimal>(type: "numeric", nullable: true, defaultValue: 0m),
                    CompletedWork = table.Column<decimal>(type: "numeric", nullable: true, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
