using System.Collections.Generic;

namespace WebAPI.InputModels
{
    public class InserirPedidoInputModel
    {
        public int ClienteId { get; set; }
        public IEnumerable<ItemDoPedidoInputModel> Itens { get; set; }
    }
}
