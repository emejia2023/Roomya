namespace Entidades;
public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Clave { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public int IdPerfil { get; set; }
    public bool Estado { get; set; } = true;
}
