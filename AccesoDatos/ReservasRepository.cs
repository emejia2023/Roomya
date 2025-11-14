using Dapper;
using Entidades;

namespace AccesoDatos;
public class ReservasRepository
{
    private readonly SqlConnectionFactory _f;
    public ReservasRepository(SqlConnectionFactory f){ _f=f; }
    public async Task<(int IdReserva, string Codigo)> CrearAsync(int idCliente, int idHabitacion, DateOnly inicio, DateOnly fin, decimal monto)
    {
        using var c=_f.Create();
        var row = await c.QueryFirstAsync("EXEC sp_Reservas_Crear @IdCliente,@IdHabitacion,@FechaInicio,@FechaFin,@Monto",
            new { IdCliente=idCliente, IdHabitacion=idHabitacion, FechaInicio=inicio, FechaFin=fin, Monto=monto });
        return (int.Parse(row.IdReserva.ToString()), (string)row.CodigoReserva);
    }
    public async Task<int> CancelarAsync(int idReserva, decimal multa){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Reservas_Cancelar @IdReserva,@Multa", new { IdReserva=idReserva, Multa=multa }); }
    public async Task<int> FinalizarAsync(int idReserva, decimal monto, string metodo){ using var c=_f.Create(); return await c.ExecuteScalarAsync<int>("EXEC sp_Reservas_Finalizar @IdReserva,@MontoPagado,@MetodoPago", new { IdReserva=idReserva, MontoPagado=monto, MetodoPago=metodo }); }
    public async Task<IEnumerable<Reserva>> PorClienteAsync(int idCliente){ using var c=_f.Create(); return await c.QueryAsync<Reserva>("EXEC sp_Reservas_PorCliente @IdCliente", new { IdCliente=idCliente }); }
    public async Task<Reserva?> PorCodigoAsync(string codigo){ using var c=_f.Create(); return (await c.QueryAsync<Reserva>("EXEC sp_Reservas_PorCodigo @Codigo", new { Codigo=codigo })).FirstOrDefault(); }
}
