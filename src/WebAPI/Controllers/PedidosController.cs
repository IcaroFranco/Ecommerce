using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.InputModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly Context _context;
        public PedidosController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InserirPedido([FromBody] InserirPedidoInputModel input)
        {
            Result<Pedido> pedido = Pedido.Criar(input.ClienteId);

            if (pedido.IsFailure)
                return BadRequest($"Erro ao inserir Pedido, {pedido.Error}");

            foreach(ItemDoPedidoInputModel inputItem in input.Itens)
            {
                Result item = pedido.Value.AddItem(inputItem.Quantidade);

                if (item.IsFailure)
                    return BadRequest($"Erro ao inserir Pedido, {item.Error}");
            }

            _context.Pedidos.Add(pedido.Value);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
