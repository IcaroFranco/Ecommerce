namespace WebAPI.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; private set; }

        private Cliente(string nome)
        {
            Nome = nome;
        }

        public static Cliente Criar(string nome)
        {


            // Validações


            return new Cliente(nome);
        }
    }
}
