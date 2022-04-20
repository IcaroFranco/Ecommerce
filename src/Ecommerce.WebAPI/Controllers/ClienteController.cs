using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Data;
using Ecommerce.WebAPI.Entities;
using Ecommerce.WebAPI.InputModels;
using Ecommerce.WebAPI.Queries.Clientes;
using Ecommerce.WebAPI.Queries.Clientes.ViewModel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.WebAPI._Core.Shared;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Context _context;
        private readonly IClientesQuery _clientesQuery;

        public ClienteController(Context context, IClientesQuery clientesQuery)
        {
            _context = context;
            _clientesQuery = clientesQuery;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> Listar([FromQuery] ClienteParameters parameters)
        {
            PagedList<ListarClienteViewModel> pagedList = await _clientesQuery.ListarClientesAsync(parameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));

            return Ok(pagedList.Data);
        }

        [HttpGet("clienteId")]
        public async Task<IActionResult> BuscarAsync(int id)
        {
            ClienteViewModel dto = await _clientesQuery.BuscarClienteAsync(id);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Criar([FromBody]InserirClienteInputModel input)
        {
            Result<Cliente> cliente = Cliente.Criar(input.Nome, input.Sobrenome, input.Email);

            if (cliente.IsFailure)
                return BadRequest();

            _context.Clientes.Add(cliente.Value);
            _context.SaveChanges();

            return Ok(cliente.Value);
        }

        [HttpPut("clienteId")]
        public IActionResult Editar(int clienteId, AtualizarClienteInputModel input)
        {
            Cliente cliente = _context.Clientes.Where(x => x.Id == clienteId).FirstOrDefault();

            if (cliente == null)
                return NoContent();

            Result resultado = cliente.Editar(input.Nome, input.Sobrenome, input.Email);
            if (resultado.IsFailure)
                return BadRequest();

            _context.Clientes.Update(cliente);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("clienteId")]
        public IActionResult Excluir(int clienteId)
        {
            Cliente cliente = _context.Clientes.Where(x => x.Id == clienteId).FirstOrDefault();
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", new string[] { "GET", "HEAD", "POST", "PUT", "DELETE" });

            return NoContent();
        }
    }
}
