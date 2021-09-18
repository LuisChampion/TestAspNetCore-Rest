using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Migration_UpdateTables_by_type_Jason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answers",
                table: "Survey",
                type: "jsonb",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answers",
                table: "Survey");
        }
    }
}
