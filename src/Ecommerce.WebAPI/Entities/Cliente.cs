using CSharpFunctionalExtensions;

namespace Ecommerce.WebAPI.Entities
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        private Cliente(string nome)
        {
            Nome = nome;
        }

        public static Result<Cliente> Criar(string nome)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório.")
            );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new Cliente(nome),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result Editar(string nome)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório"));

            return validacoes.Tap(() =>
            {
                Nome = nome;
            }).Finally(resultado => resultado);
        }
    }
}
