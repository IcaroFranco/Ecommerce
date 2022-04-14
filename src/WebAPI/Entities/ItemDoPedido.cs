using CSharpFunctionalExtensions;

namespace WebAPI.Entities
{
    public class ItemDoPedido
    {
        public int Id { get; private set; }
        public int Quantidade { get; private set; }
        public int PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }

        private ItemDoPedido(int quantidade, int pedidoId)
        {
            Quantidade = quantidade;
            PedidoId = pedidoId;
        }

        public static Result<ItemDoPedido> Criar(int quantidade, int produtoId)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(quantidade <= 0, "Quantidade é obrigatória")
                );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new ItemDoPedido(quantidade, produtoId),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }
    }
}
