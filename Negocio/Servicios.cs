using Entidades;
using AccesoDatos;
namespace Negocio;
public class ClientesLN : IClientesLN
{
    private readonly ClientesRepository _repo;
    public ClientesLN(ClientesRepository r){ _repo=r; }
    public Task<IEnumerable<Cliente>> ListarAsync()=>_repo.ListarAsync();
    public Task<Cliente?> ObtenerAsync(int id)=>_repo.ObtenerAsync(id);
    public Task<int> CrearAsync(Cliente c)=>_repo.CrearAsync(c);
    public Task<int> ActualizarAsync(Cliente c)=>_repo.ActualizarAsync(c);
    public Task<int> EliminarAsync(int id)=>_repo.EliminarAsync(id);
}
public class HabitacionesLN : IHabitacionesLN
{
    private readonly HabitacionesRepository _repo;
    public HabitacionesLN(HabitacionesRepository r){ _repo=r; }
    public Task<IEnumerable<Habitacion>> ListarAsync(string? estado=null)=>_repo.ListarAsync(estado);
    public Task<int> CrearAsync(Habitacion h)=>_repo.CrearAsync(h);
    public Task<int> ActualizarAsync(Habitacion h)=>_repo.ActualizarAsync(h);
    public Task<int> EliminarAsync(int id)=>_repo.EliminarAsync(id);
}
public class ReservasLN : IReservasLN
{
    private readonly ReservasRepository _repo;
    public ReservasLN(ReservasRepository r){ _repo=r; }
    public Task<(int, string)> CrearAsync(int idCliente, int idHabitacion, DateOnly inicio, DateOnly fin, decimal monto)=>_repo.CrearAsync(idCliente,idHabitacion,inicio,fin,monto);
    public Task<int> CancelarAsync(int idReserva, decimal multa)=>_repo.CancelarAsync(idReserva,multa);
    public Task<int> FinalizarAsync(int idReserva, decimal monto, string metodo)=>_repo.FinalizarAsync(idReserva,monto,metodo);
    public Task<IEnumerable<Reserva>> PorClienteAsync(int idCliente)=>_repo.PorClienteAsync(idCliente);
    public Task<Reserva?> PorCodigoAsync(string codigo)=>_repo.PorCodigoAsync(codigo);
}
