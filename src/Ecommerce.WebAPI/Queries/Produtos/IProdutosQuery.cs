using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Produtos.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Produtos
{
    public interface IProdutosQuery
    {
        Task<PagedList<ListarProdutoViewModel>> ListarProdutosAsync(ProdutoParameters parameters);
        Task<ProdutoViewModel> BuscarProdutosAsync(int produtoId);
    }
}
