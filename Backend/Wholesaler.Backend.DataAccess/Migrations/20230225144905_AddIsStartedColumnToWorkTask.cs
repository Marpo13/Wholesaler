using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    public partial class AddIsStartedColumnToWorkTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "WorkTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "WorkTasks");
        }
    }
}
