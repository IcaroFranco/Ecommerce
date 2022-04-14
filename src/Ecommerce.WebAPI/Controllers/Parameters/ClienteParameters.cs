using Ecommerce.WebAPI._Core.PagedList;

namespace Ecommerce.WebAPI.Controllers.Parameters
{
    public class ClienteParameters : RequestParameters
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public override bool HasParameters()
        {
            return !string.IsNullOrEmpty(Id) ||
                   !string.IsNullOrEmpty(Nome);
        }

        public override void SetParameters(string value)
        {
            if (string.IsNullOrEmpty(Id))
                Id = value;
            if (string.IsNullOrEmpty(Nome))
                Nome = value; 
        }
    }
}
