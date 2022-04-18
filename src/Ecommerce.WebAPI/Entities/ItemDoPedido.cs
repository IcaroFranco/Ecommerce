using CSharpFunctionalExtensions;

namespace Ecommerce.WebAPI.Entities
{
    public class ItemDoPedido
    {
        private ItemDoPedido(int quantidade, int pedidoId)
        {
            Quantidade = quantidade;
            PedidoId = pedidoId;
        }
        public int Id { get; private set; }
        public int Quantidade { get; private set; }
        public int PedidoId { get; private set; }
        public int ProdutoId { get; private set; }

        public static Result<ItemDoPedido> Criar(int quantidade, int produtoId)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(quantidade <= 0, "Quantidade é obrigatória"),
                Result.FailureIf(produtoId <= 0, "Produto é obrigatório")
                );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new ItemDoPedido(quantidade, produtoId),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result Atualizar(int quantidade)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(quantidade <= 0, "Quantidade é obrigatória")
                );

            return validacoes.Tap(() =>
            {
                Quantidade = quantidade;
            }).Finally(result => result);
        }
    }
}
