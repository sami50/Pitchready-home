using Microsoft.EntityFrameworkCore.Migrations;

namespace Empite.PitchReady.Web.Data.Migrations
{
    public partial class addingclientsLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");
        }
    }
}
