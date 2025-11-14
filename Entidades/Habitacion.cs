namespace Entidades;
public class Habitacion
{
    public int IdHabitacion { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int IdTipoHabitacion { get; set; }
    public string Estado { get; set; } = "Libre"; // Libre, Reservada, Ocupada
}
