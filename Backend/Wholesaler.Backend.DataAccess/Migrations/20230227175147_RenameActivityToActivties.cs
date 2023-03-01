using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    public partial class RenameActivityToActivties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_People_PersonId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_WorkTasks_WorkTaskId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_WorkTaskId",
                table: "Activities",
                newName: "IX_Activities_WorkTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_PersonId",
                table: "Activities",
                newName: "IX_Activities_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_People_PersonId",
                table: "Activities",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_WorkTasks_WorkTaskId",
                table: "Activities",
                column: "WorkTaskId",
                principalTable: "WorkTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_People_PersonId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_WorkTasks_WorkTaskId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_WorkTaskId",
                table: "Activity",
                newName: "IX_Activity_WorkTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_PersonId",
                table: "Activity",
                newName: "IX_Activity_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_People_PersonId",
                table: "Activity",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_WorkTasks_WorkTaskId",
                table: "Activity",
                column: "WorkTaskId",
                principalTable: "WorkTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
