using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTrack.Infrastrutture.Migrations
{
    /// <inheritdoc />
    public partial class MigrazioneIniziale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruolo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Progetti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceAccesso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DataInizioProgetto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFineProgetto = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progetti_Utenti_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgettiUtente",
                columns: table => new
                {
                    IdProgetto = table.Column<int>(type: "int", nullable: false),
                    IdUtente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgettiUtente", x => new { x.IdProgetto, x.IdUtente });
                    table.ForeignKey(
                        name: "FK_ProgettiUtente_Progetti_IdProgetto",
                        column: x => x.IdProgetto,
                        principalTable: "Progetti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgettiUtente_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrioritàTask = table.Column<int>(type: "int", nullable: true),
                    DataInizioTask = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFineTask = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProgetto = table.Column<int>(type: "int", nullable: false),
                    StatoTask = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Progetti_IdProgetto",
                        column: x => x.IdProgetto,
                        principalTable: "Progetti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskProgettoUtente",
                columns: table => new
                {
                    AttivitaId = table.Column<int>(type: "int", nullable: false),
                    UtentiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProgettoUtente", x => new { x.AttivitaId, x.UtentiId });
                    table.ForeignKey(
                        name: "FK_TaskProgettoUtente_Tasks_AttivitaId",
                        column: x => x.AttivitaId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskProgettoUtente_Utenti_UtentiId",
                        column: x => x.UtentiId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progetti_AdminId",
                table: "Progetti",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgettiUtente_IdUtente",
                table: "ProgettiUtente",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_TaskProgettoUtente_UtentiId",
                table: "TaskProgettoUtente",
                column: "UtentiId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdProgetto",
                table: "Tasks",
                column: "IdProgetto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgettiUtente");

            migrationBuilder.DropTable(
                name: "TaskProgettoUtente");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Progetti");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
