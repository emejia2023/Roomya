namespace Entidades;
public class Reserva
{
    public int IdReserva { get; set; }
    public string CodigoReserva { get; set; } = string.Empty;
    public int IdCliente { get; set; }
    public int IdHabitacion { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public decimal Monto { get; set; }
    public string Estado { get; set; } = "Activa"; // Activa, Cancelada, Finalizada
}
