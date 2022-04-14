using System.Data.Common;

namespace Ecommerce.WebAPI.Queries
{
    public interface ISqlConnectionAccessor
    {
        DbConnection Conexao();
    }
}
