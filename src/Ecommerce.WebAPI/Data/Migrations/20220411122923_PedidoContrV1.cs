using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.WebAPI.Data.Migrations
{
    public partial class PedidoContrV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "ItemDoPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "ItemDoPedido");
        }
    }
}
