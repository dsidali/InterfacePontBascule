using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class addComPortTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComPorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaudeRate = table.Column<int>(type: "int", nullable: false),
                    DataBits = table.Column<int>(type: "int", nullable: false),
                    ReceivedBytesThreshold = table.Column<int>(type: "int", nullable: false),
                    DtrEnable = table.Column<bool>(type: "bit", nullable: false),
                    RtsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComPorts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComPorts");
        }
    }
}
