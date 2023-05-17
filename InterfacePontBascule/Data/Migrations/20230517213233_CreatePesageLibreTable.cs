using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePesageLibreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pesages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcId = table.Column<int>(type: "int", nullable: false),
                    NumBonA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumTicket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transporteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDeTransportId = table.Column<int>(type: "int", nullable: false),
                    TypeDeCamionId = table.Column<int>(type: "int", nullable: false),
                    Mat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PCC = table.Column<int>(type: "int", nullable: false),
                    PCV = table.Column<int>(type: "int", nullable: false),
                    QP = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pesages_Parcs_ParcId",
                        column: x => x.ParcId,
                        principalTable: "Parcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pesages_TypeDeCamions_TypeDeCamionId",
                        column: x => x.TypeDeCamionId,
                        principalTable: "TypeDeCamions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pesages_TypeDeTransports_TypeDeTransportId",
                        column: x => x.TypeDeTransportId,
                        principalTable: "TypeDeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pesages_ParcId",
                table: "Pesages",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_Pesages_TypeDeCamionId",
                table: "Pesages",
                column: "TypeDeCamionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pesages_TypeDeTransportId",
                table: "Pesages",
                column: "TypeDeTransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pesages");
        }
    }
}
