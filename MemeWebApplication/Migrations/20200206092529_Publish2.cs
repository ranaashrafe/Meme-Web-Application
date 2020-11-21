using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeWebApplication.Migrations
{
    public partial class Publish2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ActorTemplates_ActorID",
                table: "ActorTemplates",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorTemplates_TemplateID",
                table: "ActorTemplates",
                column: "TemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorTemplates_Actors_ActorID",
                table: "ActorTemplates",
                column: "ActorID",
                principalTable: "Actors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorTemplates_Templates_TemplateID",
                table: "ActorTemplates",
                column: "TemplateID",
                principalTable: "Templates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorTemplates_Actors_ActorID",
                table: "ActorTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorTemplates_Templates_TemplateID",
                table: "ActorTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ActorTemplates_ActorID",
                table: "ActorTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ActorTemplates_TemplateID",
                table: "ActorTemplates");
        }
    }
}
