using Ecommerce.WebAPI._Core.PagedList;
using Ecommerce.WebAPI.Controllers.Parameters;
using Ecommerce.WebAPI.Queries.Clientes.ViewModel;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Queries.Clientes
{
    public interface IClientesQuery
    {
        Task<PagedList<ClienteViewModel>> ListarClientesAsync(ClienteParameters parameters);
    }
}
