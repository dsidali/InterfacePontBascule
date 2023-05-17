using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class changediametretype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Diametre",
                table: "ReceptionRondBetons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Diametre",
                table: "ReceptionRondBetons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
