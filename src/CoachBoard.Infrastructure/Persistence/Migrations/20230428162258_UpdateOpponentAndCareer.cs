using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachBoard.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOpponentAndCareer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CareerId",
                table: "Opponents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Opponents_CareerId",
                table: "Opponents",
                column: "CareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opponents_Careers_CareerId",
                table: "Opponents",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opponents_Careers_CareerId",
                table: "Opponents");

            migrationBuilder.DropIndex(
                name: "IX_Opponents_CareerId",
                table: "Opponents");

            migrationBuilder.DropColumn(
                name: "CareerId",
                table: "Opponents");
        }
    }
}
