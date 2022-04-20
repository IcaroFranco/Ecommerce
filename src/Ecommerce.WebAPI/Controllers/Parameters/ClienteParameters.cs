using Ecommerce.WebAPI._Core.PagedList;

namespace Ecommerce.WebAPI.Controllers.Parameters
{
    public class ClienteParameters : RequestParameters
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }

        public override bool HasParameters()
        {
            return !string.IsNullOrEmpty(Id) ||
                   !string.IsNullOrEmpty(Nome) ||
                   !string.IsNullOrEmpty(Sobrenome) ||
                   !string.IsNullOrEmpty(Email);
        }

        public override void SetParameters(string value)
        {
            if (string.IsNullOrEmpty(Id))
                Id = value;
            if (string.IsNullOrEmpty(Nome))
                Nome = value;
            if (string.IsNullOrEmpty(Sobrenome))
                Sobrenome = value;
            if (string.IsNullOrEmpty(Email))
                Email = value; 
        }
    }
}
