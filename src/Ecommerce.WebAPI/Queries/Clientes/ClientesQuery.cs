using Ecommerce.WebAPI.Queries.Clientes.ViewModel;
using System.Threading.Tasks;
using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;

namespace Ecommerce.WebAPI.Queries.Clientes
{
    public class ClientesQuery : IClientesQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAcessor;

        public ClientesQuery(ISqlConnectionAccessor connectionAcessor)
        {
            _ConnectionAcessor = connectionAcessor;
        }

        public async Task<PagedList<ClienteViewModel>> ListarClientesAsync(ClienteParameters parameters)
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

            PagedQuery<ClienteViewModel> pagedQuery = await conexao.PagedQueryAsync<ClienteViewModel>(query, pagination);
            return new PagedList<ClienteViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }
    }
}
