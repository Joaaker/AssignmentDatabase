using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSetforProjectServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectServiceJunctionEntity_Projects_ProjectId",
                table: "ProjectServiceJunctionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectServiceJunctionEntity_Services_ServiceId",
                table: "ProjectServiceJunctionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectServiceJunctionEntity",
                table: "ProjectServiceJunctionEntity");

            migrationBuilder.RenameTable(
                name: "ProjectServiceJunctionEntity",
                newName: "ProjectServices");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectServiceJunctionEntity_ServiceId",
                table: "ProjectServices",
                newName: "IX_ProjectServices_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectServices",
                table: "ProjectServices",
                columns: new[] { "ProjectId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectServices_Projects_ProjectId",
                table: "ProjectServices",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectServices_Services_ServiceId",
                table: "ProjectServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectServices_Projects_ProjectId",
                table: "ProjectServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectServices_Services_ServiceId",
                table: "ProjectServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectServices",
                table: "ProjectServices");

            migrationBuilder.RenameTable(
                name: "ProjectServices",
                newName: "ProjectServiceJunctionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectServices_ServiceId",
                table: "ProjectServiceJunctionEntity",
                newName: "IX_ProjectServiceJunctionEntity_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectServiceJunctionEntity",
                table: "ProjectServiceJunctionEntity",
                columns: new[] { "ProjectId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectServiceJunctionEntity_Projects_ProjectId",
                table: "ProjectServiceJunctionEntity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectServiceJunctionEntity_Services_ServiceId",
                table: "ProjectServiceJunctionEntity",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
