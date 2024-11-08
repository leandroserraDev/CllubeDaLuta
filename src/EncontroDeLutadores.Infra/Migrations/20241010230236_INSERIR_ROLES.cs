using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncontroDeLutadores.Infra.Migrations
{
    public partial class INSERIR_ROLES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                 table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[]
                     { "1", "Administrador", "ADMINISTRADOR"});

            migrationBuilder.InsertData(
           table: "AspNetRoles",
          columns: new[] { "Id", "Name", "NormalizedName" },
          values: new object[]
           { "2", "Mestre", "MESTRE"});

            migrationBuilder.InsertData(
           table: "AspNetRoles",
          columns: new[] { "Id", "Name", "NormalizedName" },
          values: new object[] { "3", "Lutador", "LUTADOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "AspNetRoles", "Id", new object[] { "1", "2", "3" });
        }
    }
}
