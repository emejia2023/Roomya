using Dapper;
using Entidades;

namespace AccesoDatos;
public class AuthRepository
{
    private readonly SqlConnectionFactory _factory;
    public AuthRepository(SqlConnectionFactory f) => _factory=f;

    public async Task<(Usuario user, string perfil)?> LoginAsync(string usuario, string clave)
    {
        using var conn = _factory.Create();
        var rows = await conn.QueryAsync(@"EXEC sp_Auth_Login @NombreUsuario, @ClavePlano",
            new { NombreUsuario = usuario, ClavePlano = clave });
        var row = rows.FirstOrDefault();
        if (row is null) return null;
        return (new Usuario{
            IdUsuario = (int)row.IdUsuario,
            NombreUsuario = (string)row.NombreUsuario,
            NombreCompleto = (string)row.NombreCompleto
        }, (string)row.Perfil);
    }
}
