using CSharpFunctionalExtensions;

namespace Ecommerce.WebAPI.Entities
{
    public class Produto
    {
        public Produto(string nome, string descricao, double preco)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Preco { get; private set; }

        public static Result<Produto> Criar(string nome, string descricao, double preco)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório."),
                Result.FailureIf(string.IsNullOrEmpty(descricao), "Descrição é obrigatório."),
                Result.FailureIf(preco <= 0, "Preço é obrigatório.")
            );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new Produto(nome, descricao, preco),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result Editar(string nome, string descricao, double preco)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório"),
                Result.FailureIf(string.IsNullOrEmpty(descricao), "Descrição é obrigatório"),
                Result.FailureIf(preco <= 0, "Preço é obrigatório.")
                );

            return validacoes.Tap(() =>
            {
                Nome = nome;
                Descricao = descricao;
                Preco = preco;
            }).Finally(resultado => resultado);
        }
    }
}
