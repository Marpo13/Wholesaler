using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations;

public partial class AddDelivery : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Delivery",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Delivery", x => x.Id);
                table.ForeignKey(
                    name: "FK_Delivery_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Delivery_PersonId",
            table: "Delivery",
            column: "PersonId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Delivery");
    }
}
