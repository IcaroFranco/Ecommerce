using CSharpFunctionalExtensions;
using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Data;
using Ecommerce.WebAPI.Entities;
using Ecommerce.WebAPI.InputModels;
using Ecommerce.WebAPI.Queries.Produtos;
using Ecommerce.WebAPI.Queries.Produtos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly Context _context;
        private readonly IProdutosQuery _produtosQuery;

        public ProdutosController(Context context, IProdutosQuery produtosQuery)
        {
            _context = context;
            _produtosQuery = produtosQuery;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> Listar([FromQuery] ProdutoParameters parameters)
        {
            PagedList<ListarProdutoViewModel> pagedList = await _produtosQuery.ListarProdutosAsync(parameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));

            return Ok(pagedList.Data);
        }

        [HttpGet("produtoId")]
        public async Task<IActionResult> BuscarAsync(int produtoId)
        {
            ProdutoViewModel dto = await _produtosQuery.BuscarProdutosAsync(produtoId);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] InserirProdutoInputModel input)
        {
            Result<Produto> produto = Produto.Criar(input.Nome, input.Descricao, input.Preco);

            if(produto.IsFailure)
                return NotFound();

            _context.Produtos.Add(produto.Value);
            _context.SaveChanges();

            return Ok(produto.Value);
        }

        [HttpPut("produtoId")]
        public IActionResult Editar(int produtoId, AtualizarProdutoInputModel input)
        {
            Produto produto = _context.Produtos.Where(x => x.Id == produtoId).FirstOrDefault();

            if (produto == null)
                return NotFound();

            Result resultado = produto.Editar(input.Nome, input.Descricao, input.Preco);
            if (resultado.IsFailure)
                return BadRequest();

            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("produtoId")]
        public IActionResult Excluir(int produtoId)
        {
            Produto produto = _context.Produtos.Where(x => x.Id == produtoId).FirstOrDefault();
            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", new string[] { "GET", "HEAD", "POST", "PUT", "DELETE"});
            
            return NoContent();
        }
    }
}
