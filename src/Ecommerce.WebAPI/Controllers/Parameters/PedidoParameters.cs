using Ecommerce.WebAPI._Core.PagedList;

namespace Ecommerce.WebAPI.Controllers.Parameters
{
    public class PedidoParameters : RequestParameters
    {
        public string Id { get; set; }
        public string ClienteId { get; set; }

        public override bool HasParameters()
        {
            return !string.IsNullOrEmpty(Id) ||
                   !string.IsNullOrEmpty(ClienteId);
        }

        public override void SetParameters(string value)
        {
            if (string.IsNullOrEmpty(Id))
                Id = value;
            if (string.IsNullOrEmpty(ClienteId))
                ClienteId = value;
        }
    }
}
