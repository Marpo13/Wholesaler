using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    public partial class AddStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            var sql = "INSERT INTO Storages (Id, Name, State) VALUES ('00000000-0000-0000-0000-000000000000', 'default', 0)";
            migrationBuilder.Sql(sql);

            migrationBuilder.AddColumn<Guid>(
                name: "StorageId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_StorageId",
                table: "Requirements",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Storages_StorageId",
                table: "Requirements",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Storages_StorageId",
                table: "Requirements");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_StorageId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Requirements");
        }
    }
}
