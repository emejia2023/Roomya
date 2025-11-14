using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Negocio;
using Entidades;

namespace ServicioRoomya.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize] // Todos deben estar autenticados para acceder
    public class ClientesController : ControllerBase
    {
        private readonly IClientesLN _ln;

        public ClientesController(IClientesLN ln)
        {
            _ln = ln;
        }

        // Permite listar clientes (solo autenticados)
        [HttpGet]
        public async Task<IActionResult> List()
            => Ok(await _ln.ListarAsync());

        // Obtener un cliente por ID (solo autenticados)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _ln.ObtenerAsync(id));

        // Crear cliente (Administrador o Recepcionista)
        [Authorize(Policy = "CanEditClientes")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente c)
            => Ok(new { Id = await _ln.CrearAsync(c) });

        // Actualizar cliente (Administrador o Recepcionista)
        [Authorize(Policy = "CanEditClientes")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente c)
        {
            c.IdCliente = id;
            return Ok(new { Affected = await _ln.ActualizarAsync(c) });
        }

        // Eliminar cliente (solo Administrador)
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(new { Affected = await _ln.EliminarAsync(id) });
    }
}
