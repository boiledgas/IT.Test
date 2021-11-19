// Тестовое задание https://github.com/boiledgas/IT.Test

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IT.Test.Persistence.Migrations
{
    public partial class Organization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:nocase", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                collation: "nocase",
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "nocase")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "name" },
                values: new object[] { 1, "Информационные технологии" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_name",
                table: "Organizations",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Users_email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Users");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:CollationDefinition:nocase", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldCollation: "nocase");
        }
    }
}
