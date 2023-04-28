using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachBoard.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlayerScoredId",
                table: "Goals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PlayerScoredId",
                table: "Goals",
                column: "PlayerScoredId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Players_PlayerScoredId",
                table: "Goals",
                column: "PlayerScoredId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Players_PlayerScoredId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_PlayerScoredId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "PlayerScoredId",
                table: "Goals");
        }
    }
}
