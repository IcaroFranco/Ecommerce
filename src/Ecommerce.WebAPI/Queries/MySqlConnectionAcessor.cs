using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Ecommerce.WebAPI.Queries
{
    public class MySqlConnectionAcessor : ISqlConnectionAccessor
    {
        private readonly IConfiguration _configuration;

        public MySqlConnectionAcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection Conexao()
        {
            return new MySqlConnection(_configuration.GetConnectionString("sqlConnection"));
        }
    }
}
