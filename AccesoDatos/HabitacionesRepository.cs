using Dapper;
using Entidades;

namespace AccesoDatos;
public class HabitacionesRepository
{
    private readonly SqlConnectionFactory _f;
    public HabitacionesRepository(SqlConnectionFactory f){ _f=f; }
    public async Task<IEnumerable<Habitacion>> ListarAsync(string? estado=null){ using var c=_f.Create(); return await c.QueryAsync<Habitacion>("EXEC sp_Habitaciones_Listar @Estado", new { Estado=estado }); }
    public async Task<int> CrearAsync(Habitacion h){ using var c=_f.Create(); return (int)(await c.ExecuteScalarAsync<decimal>("EXEC sp_Habitaciones_Crear @Numero,@IdTipoHabitacion,@Estado", h)); }
    public async Task<int> ActualizarAsync(Habitacion h){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Habitaciones_Actualizar @IdHabitacion,@Numero,@IdTipoHabitacion,@Estado", h); }
    public async Task<int> EliminarAsync(int id){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Habitaciones_Eliminar @IdHabitacion", new { IdHabitacion=id }); }
}
