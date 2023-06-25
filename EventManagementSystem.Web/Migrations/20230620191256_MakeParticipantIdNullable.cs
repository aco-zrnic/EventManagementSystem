using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class MakeParticipantIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "participant_id",
                table: "tickets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "participant_id",
                table: "tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true,
                oldType: "integer"
            );
        }
    }
}
