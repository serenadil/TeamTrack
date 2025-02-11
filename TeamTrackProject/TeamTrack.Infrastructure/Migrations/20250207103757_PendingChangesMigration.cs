using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTask");

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
                        name: "FK_TaskProgettoUtente_Users_UtentiId",
                        column: x => x.UtentiId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskProgettoUtente_UtentiId",
                table: "TaskProgettoUtente",
                column: "UtentiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskProgettoUtente");

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTask_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTask_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_UserId",
                table: "UserTask",
                column: "UserId");
        }
    }
}
