using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrations_Roles_RoleId",
                table: "registrations");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "registrations",
                newName: "roleId");

            migrationBuilder.RenameIndex(
                name: "IX_registrations_RoleId",
                table: "registrations",
                newName: "IX_registrations_roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_Roles_roleId",
                table: "registrations",
                column: "roleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrations_Roles_roleId",
                table: "registrations");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "registrations",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_registrations_roleId",
                table: "registrations",
                newName: "IX_registrations_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_Roles_RoleId",
                table: "registrations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
