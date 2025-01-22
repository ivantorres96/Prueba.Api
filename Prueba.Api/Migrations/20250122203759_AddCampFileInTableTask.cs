using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCampFileInTableTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Tasks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Tasks");
        }
    }
}
