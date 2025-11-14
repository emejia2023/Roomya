using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccesoDatos;
using Entidades;

[ApiController]
[Route("api/bitacora")]
[Authorize]
public class BitacoraController : ControllerBase
{
    private readonly MongoBitacora _mongo;
    public BitacoraController(MongoBitacora m){ _mongo=m; }
    [HttpGet] public async Task<IActionResult> List()=>Ok(await _mongo.ListAsync());
    [HttpPost] public async Task<IActionResult> Add([FromBody] BitacoraEntry e){ await _mongo.InsertAsync(e); return Ok(true); }
}
