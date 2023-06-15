using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    public partial class AddStatusAndDeliveryDateToRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Requirements",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requirements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requirements");
        }
    }
}
