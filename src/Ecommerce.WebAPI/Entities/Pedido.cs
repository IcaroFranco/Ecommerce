using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.WebAPI.Entities
{
    public class Pedido
    {
        private Pedido(int clienteId)
        {
            ClienteId = clienteId;
            Data = DateTime.Now;
        }
        private readonly ICollection<ItemDoPedido> _itens = new List<ItemDoPedido>();
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public int ClienteId { get; private set; }
        public IReadOnlyCollection<ItemDoPedido> Itens => _itens.ToList();

        public static Result<Pedido> Criar(int clienteId)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(clienteId <= 0, "Cliente é obrigatório"));

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new Pedido(clienteId),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result AddItem(int quantidade, int produtoId)
        {
            return ItemDoPedido.Criar(quantidade, produtoId).Tap((item) =>
            {
                _itens.Add(item);
            }).Finally(resultado => resultado);
        }
    }
}
