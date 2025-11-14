namespace Entidades;
public class Cancelacion
{
    public int IdCancelacion { get; set; }
    public int IdReserva { get; set; }
    public DateTime FechaCancelacion { get; set; } = DateTime.UtcNow;
    public decimal Multa { get; set; }
}
