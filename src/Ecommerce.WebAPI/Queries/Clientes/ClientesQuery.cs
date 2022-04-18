using Ecommerce.WebAPI.Queries.Clientes.ViewModel;
using System.Threading.Tasks;
using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Dapper;

namespace Ecommerce.WebAPI.Queries.Clientes
{
    public class ClientesQuery : IClientesQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAcessor;

        public ClientesQuery(ISqlConnectionAccessor connectionAcessor)
        {
            _ConnectionAcessor = connectionAcessor;
        }

        public async Task<PagedList<ListarClienteViewModel>> ListarClientesAsync(ClienteParameters parameters)
        {
            using var conexao = _ConnectionAcessor.Conexao();

            Pagination pagination = new(parameters);

            string query = $@"
                SELECT id
                      ,nome
                FROM clientes
                WHERE ('{parameters.Id}' = '' OR id LIKE '{parameters.Id}')
                AND ('{parameters.Nome}' = '' OR nome LIKE '%{parameters.Nome}%')
                ;
            ";

            PagedQuery<ListarClienteViewModel> pagedQuery = await conexao.PagedQueryAsync<ListarClienteViewModel>(query, pagination);
            return new PagedList<ListarClienteViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }

        public async Task<ClienteViewModel> BuscarClienteAsync(int clienteId)
        {
            using var conexao = _ConnectionAcessor.Conexao();

            string query = $@"
                SELECT id AS Id
                      ,nome AS Nome
                FROM clientes
                WHERE id = @clienteId
                ;
            ";

            return await conexao.QuerySingleOrDefaultAsync<ClienteViewModel>(query, new { clienteId });
        }
    }
}
