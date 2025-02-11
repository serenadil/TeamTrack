using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseNameTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AdminId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskProgettoUtente_Users_UtentiId",
                table: "TaskProgettoUtente");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_IdProgetto",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "UserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Utenti");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Progetti");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_AdminId",
                table: "Progetti",
                newName: "IX_Progetti_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progetti",
                table: "Progetti",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProgettiUtente_IdUtente",
                table: "ProgettiUtente",
                column: "IdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Progetti_Utenti_AdminId",
                table: "Progetti",
                column: "AdminId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskProgettoUtente_Utenti_UtentiId",
                table: "TaskProgettoUtente",
                column: "UtentiId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Progetti_IdProgetto",
                table: "Tasks",
                column: "IdProgetto",
                principalTable: "Progetti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progetti_Utenti_AdminId",
                table: "Progetti");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskProgettoUtente_Utenti_UtentiId",
                table: "TaskProgettoUtente");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Progetti_IdProgetto",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ProgettiUtente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progetti",
                table: "Progetti");

            migrationBuilder.RenameTable(
                name: "Utenti",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Progetti",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Progetti_AdminId",
                table: "Projects",
                newName: "IX_Projects_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserProject",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserId",
                table: "UserProject",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AdminId",
                table: "Projects",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskProgettoUtente_Users_UtentiId",
                table: "TaskProgettoUtente",
                column: "UtentiId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_IdProgetto",
                table: "Tasks",
                column: "IdProgetto",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
