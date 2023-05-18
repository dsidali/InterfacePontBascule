using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateReceptionTransfertRB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceptionTransfertRondBetons",
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
                    Diametre = table.Column<int>(type: "int", nullable: false),
                    PCC = table.Column<int>(type: "int", nullable: false),
                    PCV = table.Column<int>(type: "int", nullable: false),
                    PQS = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionTransfertRondBetons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertRondBetons_Parcs_ParcId",
                        column: x => x.ParcId,
                        principalTable: "Parcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertRondBetons_TypeDeCamions_TypeDeCamionId",
                        column: x => x.TypeDeCamionId,
                        principalTable: "TypeDeCamions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionTransfertRondBetons_TypeDeTransports_TypeDeTransportId",
                        column: x => x.TypeDeTransportId,
                        principalTable: "TypeDeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertRondBetons_ParcId",
                table: "ReceptionTransfertRondBetons",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertRondBetons_TypeDeCamionId",
                table: "ReceptionTransfertRondBetons",
                column: "TypeDeCamionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionTransfertRondBetons_TypeDeTransportId",
                table: "ReceptionTransfertRondBetons",
                column: "TypeDeTransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptionTransfertRondBetons");
        }
    }
}
