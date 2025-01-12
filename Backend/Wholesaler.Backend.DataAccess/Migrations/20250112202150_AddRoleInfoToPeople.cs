using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations;

public partial class AddRoleInfoToPeople : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "RoleInfo",
            table: "People",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.Sql(@"
        UPDATE People
        SET RoleInfo = JSON_QUERY('{
            ""Type"": ' + 
                CASE 
                    WHEN Role = 0 THEN '""Employee""'
                    WHEN Role = 1 THEN '""Manager""'
                    WHEN Role = 2 THEN '""Owner""'
                    ELSE '""""' 
                END + ',
            ""Name"": ' + '""' + Name + '""' + ',
            ""Surname"": ' + '""' + Surname + '""' + '
        }');
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RoleInfo",
            table: "People");
    }
}
