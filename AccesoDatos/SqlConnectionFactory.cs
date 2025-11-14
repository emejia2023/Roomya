using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AccesoDatos;
public class SqlConnectionFactory
{
    private readonly string _conn;
    public SqlConnectionFactory(IConfiguration cfg) { _conn = cfg.GetConnectionString("Sql") ?? ""; }
    public SqlConnection Create() => new SqlConnection(_conn);
}
