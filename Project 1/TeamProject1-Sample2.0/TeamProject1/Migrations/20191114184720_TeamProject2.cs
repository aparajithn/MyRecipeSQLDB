using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeamProject1.Migrations
{
    public partial class TeamProject2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyRecipe_Seasoning",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    R_id = table.Column<int>(nullable: false),
                    S_id = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRecipe_Seasoning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyRecipe_Seasoning_MyRecipe_R_id",
                        column: x => x.R_id,
                        principalTable: "MyRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyRecipe_Seasoning_Seasoning_S_id",
                        column: x => x.S_id,
                        principalTable: "Seasoning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyRecipe_Seasoning_R_id",
                table: "MyRecipe_Seasoning",
                column: "R_id");

            migrationBuilder.CreateIndex(
                name: "IX_MyRecipe_Seasoning_S_id",
                table: "MyRecipe_Seasoning",
                column: "S_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyRecipe_Seasoning");
        }
    }
}
