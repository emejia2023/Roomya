using System.Threading.Tasks;

namespace AccesoDatos
{
    /// <summary>
    /// Servicio de bitácora: registra acciones en MongoDB.
    /// </summary>
    public interface IBitacoraService
    {
        /// <param name="modulo">Ej: "Clientes", "Reservas"</param>
        /// <param name="accion">Ej: "Listar", "Crear", "Actualizar", "Eliminar"</param>
        /// <param name="usuario">Nombre de usuario que ejecuta la acción</param>
        /// <param name="ipCliente">IP del cliente (opcional)</param>
        /// <param name="extra">Objeto con info adicional (opcional), se serializa o ToString()</param>
        Task RegistrarAsync(
            string modulo,
            string accion,
            string usuario,
            string? ipCliente = null,
            object? extra = null
        );
    }
}
