using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Pedidos.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Pedidos
{
    public interface IPedidoQuery
    {
        Task<PagedList<PedidoViewModel>> ListarPedidosAsync(PedidoParameters parameters);
    }
}
