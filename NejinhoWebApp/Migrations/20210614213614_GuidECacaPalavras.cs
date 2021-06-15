using Microsoft.EntityFrameworkCore.Migrations;

namespace NejinhoWebApp.Migrations
{
    public partial class GuidECacaPalavras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CacaPalavras",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Atividades",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q1",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q10",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q2",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q3",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q4",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q5",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q6",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q7",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q8",
                table: "Atividades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Q9",
                table: "Atividades",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacaPalavras",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q1",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q10",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q2",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q3",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q4",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q5",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q6",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q7",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q8",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Q9",
                table: "Atividades");
        }
    }
}
