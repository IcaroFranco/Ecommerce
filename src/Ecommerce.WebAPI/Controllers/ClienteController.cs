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

        // TO DO: Ver com o ALM
        // [HttpHead]
        // public IActionResult Head()
        // {
        //     Response.Headers.Add("X-TotalDeRegistros", JsonConvert.SerializeObject(_context.Clientes.Count()));
           
        //     return NoContent();
        // }
        [HttpGet("clienteId")]
        public async Task<IActionResult> BuscarAsync(int id)
        {
            ClienteViewModel dto = await _clientesQuery.BuscarClienteAsync(id);

            if (dto == null)
                return BadRequest($"Cliente Id: {id} não existe.");

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Criar([FromBody]InserirClienteInputModel input)
        {
            Result<Cliente> cliente = Cliente.Criar(input.Nome);

            if (cliente.IsFailure)
                return BadRequest($"Erro ao inserir Cliente, {cliente.Error}");

            _context.Clientes.Add(cliente.Value);
            _context.SaveChanges();

            return Ok($"Cliente {cliente.Value.Nome}, Id: {cliente.Value.Id} criado com sucesso.");
        }

        [HttpPut("clienteId")]
        public IActionResult Editar(int clienteId, AtualizarClienteInputModel input)
        {
            Cliente cliente = _context.Clientes.Where(x => x.Id == clienteId).FirstOrDefault();

            if (cliente == null)
                return BadRequest($"Cliente Id: {clienteId} não existe.");

            Result resultado = cliente.Editar(input.Nome);
            if (resultado.IsFailure)
                return BadRequest($"Erro ao atualizar Cliente.");

            _context.Clientes.Update(cliente);
            _context.SaveChanges();

            return Ok($"Cliente {cliente.Nome}, Id: {cliente.Id} editado com sucesso.");
        }

        [HttpDelete("clienteId")]
        public IActionResult Excluir(int clienteId)
        {
            Cliente cliente = _context.Clientes.Where(x => x.Id == clienteId).FirstOrDefault();
            if (cliente == null)
                return BadRequest($"Cliente Id: {clienteId} não existe.");

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok($"Cliente {cliente.Nome}, Id: {cliente.Id} excluido com sucesso.");
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", new string[] { "GET", "HEAD", "POST", "PUT", "DELETE" });

            return NoContent();
        }
    }
}
