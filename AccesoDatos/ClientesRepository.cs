using Dapper;
using Entidades;

namespace AccesoDatos;
public class ClientesRepository
{
    private readonly SqlConnectionFactory _f;
    public ClientesRepository(SqlConnectionFactory f){ _f=f; }
    public async Task<IEnumerable<Cliente>> ListarAsync(){ using var c=_f.Create(); return await c.QueryAsync<Cliente>("EXEC sp_Clientes_Listar"); }
    public async Task<Cliente?> ObtenerAsync(int id){ using var c=_f.Create(); return (await c.QueryAsync<Cliente>("EXEC sp_Clientes_Obtener @IdCliente", new { IdCliente=id })).FirstOrDefault(); }
    public async Task<int> CrearAsync(Cliente cli){ using var c=_f.Create(); return (int)(await c.ExecuteScalarAsync<decimal>("EXEC sp_Clientes_Crear @Cedula,@Nombre,@Apellido,@Telefono,@Email", cli)); }
    public async Task<int> ActualizarAsync(Cliente cli){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Clientes_Actualizar @IdCliente,@Cedula,@Nombre,@Apellido,@Telefono,@Email", cli); }
    public async Task<int> EliminarAsync(int id){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Clientes_Eliminar @IdCliente", new { IdCliente=id }); }
}
