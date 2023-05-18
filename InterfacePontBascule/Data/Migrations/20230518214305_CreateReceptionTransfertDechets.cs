using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateReceptionTransfertDechets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceptionTransfertDechets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcId = table.Column<int>(type: "int", nullable: false),
                    NumBL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Transporteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provenance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDeTransportId = table.Column<int>(type: "int", nullable: false),
                    TypeDeCamionId = table.Column<int>(type: "int", nullable: false),
                    Mat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDeDechetId = table.Column<int>(type: "int", nullable: false),
                    PCC = table.Column<int>(type: "int", nullable: false),
                    PCV = table.Column<int>(type: "int", nullable: false),
                    PQS = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionTransfertDechets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertDechets_Parcs_ParcId",
                        column: x => x.ParcId,
                        principalTable: "Parcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertDechets_TypeDeCamions_TypeDeCamionId",
                        column: x => x.TypeDeCamionId,
                        principalTable: "TypeDeCamions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertDechets_TypeDeDechets_TypeDeDechetId",
                        column: x => x.TypeDeDechetId,
                        principalTable: "TypeDeDechets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertDechets_TypeDeTransports_TypeDeTransportId",
                        column: x => x.TypeDeTransportId,
                        principalTable: "TypeDeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertDechets_ParcId",
                table: "ReceptionTransfertDechets",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertDechets_TypeDeCamionId",
                table: "ReceptionTransfertDechets",
                column: "TypeDeCamionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertDechets_TypeDeDechetId",
                table: "ReceptionTransfertDechets",
                column: "TypeDeDechetId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertDechets_TypeDeTransportId",
                table: "ReceptionTransfertDechets",
                column: "TypeDeTransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptionTransfertDechets");
        }
    }
}
