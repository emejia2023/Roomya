using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Negocio;
using Entidades;

[ApiController]
[Route("api/habitaciones")]
[Authorize]
public class HabitacionesController : ControllerBase
{
    private readonly IHabitacionesLN _ln;
    public HabitacionesController(IHabitacionesLN ln){ _ln=ln; }
    [HttpGet] public async Task<IActionResult> List([FromQuery] string? estado=null)=>Ok(await _ln.ListarAsync(estado));
    [Authorize(Roles="Administrador")] [HttpPost] public async Task<IActionResult> Create([FromBody] Habitacion h)=>Ok(new{ Id=await _ln.CrearAsync(h)});
    [Authorize(Roles="Administrador")] [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, [FromBody] Habitacion h){ h.IdHabitacion=id; return Ok(new{ Affected=await _ln.ActualizarAsync(h)});}
    [Authorize(Roles="Administrador")] [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id)=>Ok(new{ Affected=await _ln.EliminarAsync(id)});
}
