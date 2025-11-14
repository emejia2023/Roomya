using Dapper;

namespace AccesoDatos;
public class ReportesRepository
{
    private readonly SqlConnectionFactory _f;
    public ReportesRepository(SqlConnectionFactory f){ _f=f; }
    public async Task<decimal> RecaudacionAsync(DateOnly desde, DateOnly hasta){ using var c=_f.Create(); return await c.ExecuteScalarAsync<decimal>("EXEC sp_Reportes_Recaudacion @Desde,@Hasta", new { Desde=desde, Hasta=hasta }); }
    public async Task<IEnumerable<dynamic>> EstadoHabitacionesAsync(){ using var c=_f.Create(); return await c.QueryAsync("EXEC sp_Habitaciones_Estado"); }
}
