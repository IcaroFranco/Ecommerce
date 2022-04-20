using Ecommerce.WebAPI._Core.PagedList;

namespace Ecommerce.WebAPI.Controllers.Parameters
{
    public class ProdutoParameters : RequestParameters
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; }

        public override bool HasParameters()
        {
            return !string.IsNullOrEmpty(Id) ||
                   !string.IsNullOrEmpty(Nome) ||
                   !string.IsNullOrEmpty(Descricao) ||
                   !string.IsNullOrEmpty(Preco);
        }

        public override void SetParameters(string value)
        {
            if (string.IsNullOrEmpty(Id))
                Id = value;
            if (string.IsNullOrEmpty(Nome))
                Nome = value;
            if (string.IsNullOrEmpty(Descricao))
                Descricao = value;
            if (string.IsNullOrEmpty(Preco))
                Preco = value;
        }
    }
}
