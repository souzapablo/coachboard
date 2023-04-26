using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachBoard.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdatePlayerModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "KitNumber",
            table: "Players",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<DateTime>(
            name: "JoinedDate",
            table: "Players",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "Players",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            table: "Players");

        migrationBuilder.AlterColumn<int>(
            name: "KitNumber",
            table: "Players",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "JoinedDate",
            table: "Players",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);
    }
}
