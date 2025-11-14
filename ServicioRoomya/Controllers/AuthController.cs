using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using AccesoDatos;

namespace ServicioRoomya.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly AuthRepository _repo;
        private readonly IConfiguration _cfg;

        public AuthController(AuthRepository repo, IConfiguration cfg)
        {
            _repo = repo;
            _cfg = cfg;
        }

        public record LoginReq(string usuario, string clave);

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginReq req)
        {
            if (string.IsNullOrWhiteSpace(req.usuario) || string.IsNullOrWhiteSpace(req.clave))
                return BadRequest("Usuario y clave son requeridos.");

            // Autenticar contra SQL (sp_Auth_Login en texto plano)
            var login = await _repo.LoginAsync(req.usuario, req.clave);
            if (login is null)
                return Unauthorized("Credenciales inválidas.");

            var (user, perfilRaw) = login.Value;

            // Normaliza el perfil a los valores que usan las Policies
            var perfil = (perfilRaw ?? string.Empty).Trim();

            // Mapear variantes comunes a "Recepcionista"
            if (perfil.Equals("Operación", StringComparison.OrdinalIgnoreCase)
                || perfil.Equals("Operacion", StringComparison.OrdinalIgnoreCase)
                || perfil.Equals("Recepción", StringComparison.OrdinalIgnoreCase)
                || perfil.Equals("Recepcion", StringComparison.OrdinalIgnoreCase)
                || perfil.Equals("Recepcionista", StringComparison.OrdinalIgnoreCase))
            {
                perfil = "Recepcionista";
            }
            // Mapear variantes comunes a "Administrador"
            else if (perfil.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                perfil = "Administrador";
            }
         

            // Claims (Program.cs ya mapea RoleClaimType/NameClaimType)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.NombreUsuario),
                new Claim(ClaimTypes.Role, perfil)
            };

            // Clave JWT (usar MISMA key que en Program.cs: "jwt:key")
            var jwtKey = _cfg["jwt:key"] ?? "SuperSecretKeyForRoomya_ChangeThis";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenStr, perfil });
        }
    }
}
