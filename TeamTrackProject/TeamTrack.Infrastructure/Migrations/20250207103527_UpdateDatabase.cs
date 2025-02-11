using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Ruolo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tasks",
                newName: "IdProgetto");

            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "Tasks",
                newName: "DataInizioTask");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tasks",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "EndingDate",
                table: "Tasks",
                newName: "DataFineTask");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "Descrizione");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "AccessCode",
                table: "Projects",
                newName: "CodiceAccesso");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PrioritàTask",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatoTask",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdProgetto",
                table: "Tasks",
                column: "IdProgetto");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_IdProgetto",
                table: "Tasks",
                column: "IdProgetto",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_IdProgetto",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IdProgetto",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PrioritàTask",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StatoTask",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Ruolo",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Tasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IdProgetto",
                table: "Tasks",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Descrizione",
                table: "Tasks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DataInizioTask",
                table: "Tasks",
                newName: "StartingDate");

            migrationBuilder.RenameColumn(
                name: "DataFineTask",
                table: "Tasks",
                newName: "EndingDate");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CodiceAccesso",
                table: "Projects",
                newName: "AccessCode");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
