using Dapper;
using System.Linq;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI._Core.PagedList
{
    public static class DbConnectionExtension
    {
        private static string GetQueryPagination(string originalQuery, Pagination pagination)
        {
            string paginationQuery = $"LIMIT {pagination.PageSize} OFFSET {(pagination.PageNumber - 1) * pagination.PageSize};";
            string query = originalQuery.Contains(";")
                ? originalQuery.Replace(";", paginationQuery)
                : originalQuery + paginationQuery;

            return query;
        }

        private static string GetCountQuery(string originalQuery)
        {
            string countQuery = originalQuery;
            Match matchResult = Regex.Match(countQuery, @"SELECT([\s\S]*?)FROM", RegexOptions.IgnoreCase);

            countQuery = countQuery.Replace(
                matchResult.Value,
                "SELECT COUNT(*) FROM"
            );

            return countQuery;
        }

        public static PagedQuery<T> PagedQuery<T>(this DbConnection connection, string query, Pagination pagination, object param = null)
        {
            return new PagedQuery<T>(
                connection.Query<T>(GetQueryPagination(query, pagination), param),
                connection.Query(GetCountQuery(query), param).Count()
            );
        }

        public static async Task<PagedQuery<T>> PagedQueryAsync<T>(this DbConnection connection, string query, Pagination pagination, object param = null)
        {
            return new PagedQuery<T>(
                await connection.QueryAsync<T>(GetQueryPagination(query, pagination), param),
                await connection.QueryFirstAsync<int>(GetCountQuery(query), param)
            );
        }
    }
}
