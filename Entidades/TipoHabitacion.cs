namespace Entidades;
public class TipoHabitacion
{
    public int IdTipoHabitacion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PrecioBase { get; set; }
    public int Capacidad { get; set; }
}
