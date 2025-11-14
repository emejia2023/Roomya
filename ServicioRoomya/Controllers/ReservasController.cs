using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Negocio;

[ApiController]
[Route("api/reservas")]
[Authorize]
public class ReservasController : ControllerBase
{
    private readonly IReservasLN _ln;
    public ReservasController(IReservasLN ln){ _ln=ln; }
    public record CrearReq(int IdCliente, int IdHabitacion, DateOnly FechaInicio, DateOnly FechaFin, decimal Monto);
    public record CancelarReq(decimal Multa);
    public record FinalizarReq(decimal MontoPagado, string MetodoPago);

    [HttpPost] public async Task<IActionResult> Crear([FromBody] CrearReq r)=>Ok(await _ln.CrearAsync(r.IdCliente,r.IdHabitacion,r.FechaInicio,r.FechaFin,r.Monto));
    [HttpPut("{id:int}/cancelar")] public async Task<IActionResult> Cancelar(int id, [FromBody] CancelarReq r)=>Ok(new{ Affected=await _ln.CancelarAsync(id,r.Multa)});
    [HttpPut("{id:int}/finalizar")] public async Task<IActionResult> Finalizar(int id, [FromBody] FinalizarReq r)=>Ok(new{ Affected=await _ln.FinalizarAsync(id,r.MontoPagado,r.MetodoPago)});
    [HttpGet("por-cliente/{idCliente:int}")] public async Task<IActionResult> PorCliente(int idCliente)=>Ok(await _ln.PorClienteAsync(idCliente));
    [HttpGet("por-codigo/{codigo}")] public async Task<IActionResult> PorCodigo(string codigo)=>Ok(await _ln.PorCodigoAsync(codigo));
}
