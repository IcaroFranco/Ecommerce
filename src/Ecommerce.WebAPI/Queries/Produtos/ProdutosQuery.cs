using Dapper;
using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Produtos.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Produtos
{
    public class ProdutosQuery : IProdutosQuery
    {
        private readonly ISqlConnectionAccessor _ConnectionAcessor;

        public ProdutosQuery(ISqlConnectionAccessor connectionAccessor)
        {
            _ConnectionAcessor = connectionAccessor;
        }

        public async Task<PagedList<ListarProdutoViewModel>> ListarProdutosAsync(ProdutoParameters parameters)
        {
            using var conexao = _ConnectionAcessor.Conexao();

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

            PagedQuery<ListarProdutoViewModel> pagedQuery = await conexao.PagedQueryAsync<ListarProdutoViewModel>(query, pagination);
            return new PagedList<ListarProdutoViewModel>(pagedQuery.Data, pagination, pagedQuery.TotalRecords);
        }
        public async Task<ProdutoViewModel> BuscarProdutosAsync(int produtoId)
        {
            using var conexao = _ConnectionAcessor.Conexao();

            string query = $@"
                SELECT id AS Id
                      ,nome AS Nome
                      ,descricao AS Descricao
                FROM produtos
                WHERE id = @produtoId
                ;
            ";

            return await conexao.QuerySingleOrDefaultAsync<ProdutoViewModel>(query, new { produtoId });
        }
    }
}
