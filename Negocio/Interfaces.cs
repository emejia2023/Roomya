using Entidades;
namespace Negocio;
public interface IClientesLN
{
    Task<IEnumerable<Cliente>> ListarAsync();
    Task<Cliente?> ObtenerAsync(int id);
    Task<int> CrearAsync(Cliente c);
    Task<int> ActualizarAsync(Cliente c);
    Task<int> EliminarAsync(int id);
}
public interface IHabitacionesLN
{
    Task<IEnumerable<Habitacion>> ListarAsync(string? estado=null);
    Task<int> CrearAsync(Habitacion h);
    Task<int> ActualizarAsync(Habitacion h);
    Task<int> EliminarAsync(int id);
}
public interface IReservasLN
{
    Task<(int IdReserva, string Codigo)> CrearAsync(int idCliente, int idHabitacion, DateOnly inicio, DateOnly fin, decimal monto);
    Task<int> CancelarAsync(int idReserva, decimal multa);
    Task<int> FinalizarAsync(int idReserva, decimal monto, string metodo);
    Task<IEnumerable<Reserva>> PorClienteAsync(int idCliente);
    Task<Reserva?> PorCodigoAsync(string codigo);
}
