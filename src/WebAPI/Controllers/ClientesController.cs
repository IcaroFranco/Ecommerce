using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.InputModels;
using WebAPI.Queries;
using WebAPI.Queries.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Context _context;
        private readonly ClientesQuery _clienteQuery;

        public ClientesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BuscarClientes()
        {
            return Ok(_context.Clientes.ToList());
        }

        [HttpPost]
        public IActionResult InsirirCliente([FromBody] InserirClienteInputModel input)
        {
            Cliente cliente = Cliente.Criar(input.Nome);

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("codigo")]
        public IActionResult Buscar(int codigo)
        {
            ClienteViewModel cliente = _clienteQuery.Buscar(codigo);

            return Ok(cliente);
        }
    }
}
