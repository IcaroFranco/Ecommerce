using System.Collections.Generic;

namespace Ecommerce.WebAPI.InputModels
{
    public class AtualizarPedidoInputModel
    {
        public IEnumerable<ItemDoPedidoInputModel> Itens { get; set; }
    }
}
