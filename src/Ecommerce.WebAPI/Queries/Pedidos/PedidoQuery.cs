using Dapper;
using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Pedidos.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Pedidos
{
    public class PedidoQuery : IPedidoQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAcessor;

        public PedidoQuery(ISqlConnectionAccessor connectionAccessor)
        {
            _ConnectionAcessor = connectionAccessor;
        }

        public async Task<PagedList<ListarPedidoViewModel>> ListarPedidosAsync(PedidoParameters parameters)
        {
            using var conexao = _ConnectionAcessor.Conexao();

            Pagination pagination = new(parameters);

            string query = $@"
                SELECT id
                      ,clienteid
                FROM pedidos
                WHERE ('{parameters.Id}' = '' OR id LIKE '{parameters.Id}')
                AND ('{parameters.ClienteId}' = '' OR clienteid LIKE '%{parameters.ClienteId}%')
                ;
            ";

            PagedQuery<ListarPedidoViewModel> pagedQuery = await conexao.PagedQueryAsync<ListarPedidoViewModel>(query, pagination);
            return new PagedList<ListarPedidoViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }

        public async Task<PedidoViewModel> BuscarPedidosAsync(int pedidoId)
        {
            using var conexao = _ConnectionAcessor.Conexao();

            string query = $@"
                SELECT id AS Id
                      ,clienteId AS ClienteId
                FROM pedidos
                WHERE id = @PedidoId
                ;
            ";

            return await conexao.QuerySingleOrDefaultAsync<PedidoViewModel>(query, new { pedidoId });
        }
    }
}
