using CSharpFunctionalExtensions;

namespace Ecommerce.WebAPI.Entities
{
    public class Produto
    {
        private Produto(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public static Result<Produto> Criar(string nome, string descricao)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome ou descricao é obrigatório."),
                Result.FailureIf(string.IsNullOrEmpty(descricao), "Nome ou descricao é obrigatório.")
            );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new Produto(nome, descricao),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result Editar(string nome, string descricao)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório"),
                Result.FailureIf(string.IsNullOrEmpty(descricao), "Nome é obrigatório")
                );

            return validacoes.Tap(() =>
            {
                Nome = nome;
                Descricao = descricao;
            }).Finally(resultado => resultado);
        }
    }
}
