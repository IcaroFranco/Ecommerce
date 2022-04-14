using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Data;
using Ecommerce.WebAPI.Entities;
using Ecommerce.WebAPI.InputModels;
using Ecommerce.WebAPI.Queries.Pedidos;
using Ecommerce.WebAPI.Queries.Pedidos.ViewModel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly Context _context;
        private readonly IPedidoQuery _pedidosQuery;

        public PedidosController(Context context, IPedidoQuery pedidosQuery)
        {
            _context = context;
            _pedidosQuery = pedidosQuery;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> Listar([FromQuery] PedidoParameters parameters)
        {

            PagedList<PedidoViewModel> pagedList = await _pedidosQuery.ListarPedidosAsync(parameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));

            return Ok(pagedList.Data);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] InserirPedidoInputModel input)
        {
            Result<Pedido> pedido = Pedido.Criar(input.ClienteId); 

            if(pedido.IsFailure)
                return BadRequest($"Erro ao inserir Pedido, {pedido.Error}");

            foreach(ItemDoPedidoInputModel inputItem in input.Itens)
            {
                Produto produto = _context.Produtos.Where(x => x.Id == inputItem.ProdutoId).FirstOrDefault();

                if(produto is null)
                    return BadRequest($"Produto não existe, {inputItem.ProdutoId}");

                Result item = pedido.Value.AddItem(inputItem.Quantidade, inputItem.ProdutoId);
                if (item.IsFailure)
                    return BadRequest($"Erro ao inserir Pedido, {item.Error}");
            }

            _context.Pedidos.Add(pedido.Value);
            _context.SaveChanges();

            return Ok($"Pedido Id: {pedido.Value.Id} criado com sucesso.");
        }

        [HttpDelete("PedidoId")]
        public IActionResult Deletar(int pedidoId)
        {
            Pedido pedido = _context.Pedidos.Where(x => x.Id == pedidoId).FirstOrDefault();
            if (pedido == null)
                return BadRequest($"Pedido Id: {pedidoId} não existe.");

            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();

            return Ok($"Pedido Id: {pedidoId} excluido com sucesso.");
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", new string[] { "GET", "HEAD", "POST", "DELETE" });

            return NoContent();
        }
    }
}
