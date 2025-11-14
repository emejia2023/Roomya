namespace Entidades;
public class Pago
{
    public int IdPago { get; set; }
    public int IdReserva { get; set; }
    public DateTime FechaPago { get; set; } = DateTime.UtcNow;
    public decimal MontoPagado { get; set; }
    public string MetodoPago { get; set; } = "Efectivo";
}
