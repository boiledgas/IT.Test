// Тестовое задание https://github.com/boiledgas/IT.Test

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IT.Test.Persistence.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    surname = table.Column<string>(type: "varchar(255)", nullable: false),
                    patronymic = table.Column<string>(type: "varchar(255)", nullable: true),
                    number = table.Column<string>(type: "varchar(255)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "email", "name", "number", "patronymic", "surname" },
                values: new object[,]
                {
                    { 1, "test@mail.ru", "Иван", "Н-1", "Иванович", "Иванов" },
                    { 2, "test@gmail.ru", "Петр", "Н-2", "Петрович", "Петров" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
