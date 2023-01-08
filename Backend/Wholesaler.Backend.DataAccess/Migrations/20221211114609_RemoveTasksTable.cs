using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    public partial class RemoveTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Tasks_TaskId",
                table: "People");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_People_TaskId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "People",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReguirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfRows = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Requirements_ReguirementId",
                        column: x => x.ReguirementId,
                        principalTable: "Requirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_TaskId",
                table: "People",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ReguirementId",
                table: "Tasks",
                column: "ReguirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Tasks_TaskId",
                table: "People",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
