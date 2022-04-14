using System.Collections.Generic;

namespace Ecommerce.WebAPI.InputModels
{
    public class InserirPedidoInputModel
    {
        public int ClienteId { get; set; }
        public IEnumerable<ItemDoPedidoInputModel> Itens { get; set; }
    }
}
