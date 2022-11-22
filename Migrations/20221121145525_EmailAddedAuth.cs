using Microsoft.EntityFrameworkCore.Migrations;

namespace UserEntity.Migrations
{
    public partial class EmailAddedAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Registrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Registrations");
        }
    }
}
