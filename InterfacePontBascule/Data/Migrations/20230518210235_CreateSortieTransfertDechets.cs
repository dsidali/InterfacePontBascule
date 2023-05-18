﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfacePontBascule.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSortieTransfertDechets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SortieTransfertDechets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcId = table.Column<int>(type: "int", nullable: false),
                    NumBL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Transporteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_SortieTransfertDechets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SortieTransfertDechets_Parcs_ParcId",
                        column: x => x.ParcId,
                        principalTable: "Parcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SortieTransfertDechets_TypeDeCamions_TypeDeCamionId",
                        column: x => x.TypeDeCamionId,
                        principalTable: "TypeDeCamions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SortieTransfertDechets_TypeDeDechets_TypeDeDechetId",
                        column: x => x.TypeDeDechetId,
                        principalTable: "TypeDeDechets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SortieTransfertDechets_TypeDeTransports_TypeDeTransportId",
                        column: x => x.TypeDeTransportId,
                        principalTable: "TypeDeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SortieTransfertDechets_ParcId",
                table: "SortieTransfertDechets",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_SortieTransfertDechets_TypeDeCamionId",
                table: "SortieTransfertDechets",
                column: "TypeDeCamionId");

            migrationBuilder.CreateIndex(
                name: "IX_SortieTransfertDechets_TypeDeDechetId",
                table: "SortieTransfertDechets",
                column: "TypeDeDechetId");

            migrationBuilder.CreateIndex(
                name: "IX_SortieTransfertDechets_TypeDeTransportId",
                table: "SortieTransfertDechets",
                column: "TypeDeTransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortieTransfertDechets");
        }
    }
}
