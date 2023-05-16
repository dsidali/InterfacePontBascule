using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAchatsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcId = table.Column<int>(type: "int", nullable: false),
                    NumBonA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumTicket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transporteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDeTransportId = table.Column<int>(type: "int", nullable: false),
                    TypeDeCamionId = table.Column<int>(type: "int", nullable: false),
                    TypeDeDechetId = table.Column<int>(type: "int", nullable: false),
                    DateOP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PCC = table.Column<int>(type: "int", nullable: false),
                    PCV = table.Column<int>(type: "int", nullable: false),
                    PB = table.Column<int>(type: "int", nullable: false),
                    PQRa = table.Column<int>(type: "int", nullable: false),
                    PQS = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achats_Parcs_ParcId",
                        column: x => x.ParcId,
                        principalTable: "Parcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achats_TypeDeCamions_TypeDeCamionId",
                        column: x => x.TypeDeCamionId,
                        principalTable: "TypeDeCamions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achats_TypeDeDechets_TypeDeDechetId",
                        column: x => x.TypeDeDechetId,
                        principalTable: "TypeDeDechets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achats_TypeDeTransports_TypeDeTransportId",
                        column: x => x.TypeDeTransportId,
                        principalTable: "TypeDeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achats_ParcId",
                table: "Achats",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_Achats_TypeDeCamionId",
                table: "Achats",
                column: "TypeDeCamionId");

            migrationBuilder.CreateIndex(
                name: "IX_Achats_TypeDeDechetId",
                table: "Achats",
                column: "TypeDeDechetId");

            migrationBuilder.CreateIndex(
                name: "IX_Achats_TypeDeTransportId",
                table: "Achats",
                column: "TypeDeTransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achats");
        }
    }
}
