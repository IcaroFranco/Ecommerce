﻿using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Pedidos.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Pedidos
{
    public class PedidoQuery : IPedidoQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAccessor;

        public PedidoQuery(ISqlConnectionAccessor connectionAccessor)
        {
            _ConnectionAccessor = connectionAccessor;
        }

        public async Task<PagedList<PedidoViewModel>> ListarPedidosAsync(PedidoParameters parameters)
        {
            using var conexao = _ConnectionAccessor.Conexao();

            Pagination pagination = new(parameters);

            string query = $@"
                SELECT id
                      ,clienteid
                FROM pedidos
                WHERE ('{parameters.Id}' = '' OR id LIKE '{parameters.Id}')
                AND ('{parameters.ClienteId}' = '' OR clienteid LIKE '%{parameters.ClienteId}%')
                ;
            ";

            PagedQuery<PedidoViewModel> pagedQuery = await conexao.PagedQueryAsync<PedidoViewModel>(query, pagination);
            return new PagedList<PedidoViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }
    }
}
