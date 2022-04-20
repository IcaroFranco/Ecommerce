using CSharpFunctionalExtensions;

namespace Ecommerce.WebAPI.Entities
{
    public class Cliente
    {
        public Cliente(string nome, string sobrenome, string email)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome{ get; set; }
        public string  Email{ get; set; }
  

        public static Result<Cliente> Criar(string nome, string sobrenome, string email)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório."),
                Result.FailureIf(string.IsNullOrEmpty(sobrenome), "Sobrenome é obrigatório."),
                Result.FailureIf(string.IsNullOrEmpty(email), "Email é obrigatório.")
            );

            return Result.SuccessIf(
                validacoes.IsSuccess,
                new Cliente(nome, sobrenome, email),
                validacoes.IsFailure ? validacoes.Error : string.Empty);
        }

        public Result Editar(string nome, string sobrenome, string email)
        {
            Result validacoes = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), "Nome é obrigatório"),
                Result.FailureIf(string.IsNullOrEmpty(sobrenome), "Sobrenome é obrigatório."),
                Result.FailureIf(string.IsNullOrEmpty(email), "Email é obrigatório.")
                );

            return validacoes.Tap(() =>
            {
                Nome = nome;
                Sobrenome = sobrenome;
                Email = email;
            }).Finally(resultado => resultado);
        }
    }
}
