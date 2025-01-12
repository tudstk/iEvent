using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEvent.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByPersonId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByPersonId",
                table: "Events");
        }
    }
}
