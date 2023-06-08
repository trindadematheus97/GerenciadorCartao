using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorCartao.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cartoes",
                columns: table => new
                {
                    CartaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeBanco = table.Column<string>(nullable: true),
                    NumeroCartao = table.Column<string>(nullable: true),
                    Limite = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.CartaoId);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    GastoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartaoId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.GastoId);
                    table.ForeignKey(
                        name: "FK_Gastos_Cartoes_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "Cartoes",
                        principalColumn: "CartaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CartaoId",
                table: "Gastos",
                column: "CartaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "Cartoes");
        }
    }
}
