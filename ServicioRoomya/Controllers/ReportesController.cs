using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccesoDatos;

[ApiController]
[Route("api/reportes")]
[Authorize]
public class ReportesController : ControllerBase
{
    private readonly ReportesRepository _repo;
    public ReportesController(ReportesRepository r){ _repo=r; }
    [HttpGet("habitaciones/estado")] public async Task<IActionResult> Estado()=>Ok(await _repo.EstadoHabitacionesAsync());
    [HttpGet("recaudacion")] public async Task<IActionResult> Recaudacion([FromQuery] DateOnly desde, [FromQuery] DateOnly hasta)=>Ok(new{ Desde=desde, Hasta=hasta, Total=await _repo.RecaudacionAsync(desde,hasta)});
}
