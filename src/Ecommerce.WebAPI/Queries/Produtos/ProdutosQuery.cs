using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Produtos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Produtos
{
    public class ProdutosQuery : IProdutosQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAccessor;

        public ProdutosQuery(ISqlConnectionAccessor connectionAccessor)
        {
            _ConnectionAccessor = connectionAccessor;
        }

        public async Task<PagedList<ProdutoViewModel>> ListarProdutosAsync(ProdutoParameters parameters)
        {
            using var conexao = _ConnectionAccessor.Conexao();

            Pagination pagination = new(parameters);

            string query = $@"
                SELECT id
                      ,nome
                      ,descricao
                FROM produtos
                WHERE ('{parameters.Id}' = '' OR id LIKE '{parameters.Id}')
                AND ('{parameters.Nome}' = '' OR nome LIKE '%{parameters.Nome}%')
                AND ('{parameters.Descricao}' = '' OR nome LIKE '%{parameters.Descricao}%')
                ;
            ";

            PagedQuery<ProdutoViewModel> pagedQuery = await conexao.PagedQueryAsync<ProdutoViewModel>(query, pagination);
            return new PagedList<ProdutoViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }
    }
}
