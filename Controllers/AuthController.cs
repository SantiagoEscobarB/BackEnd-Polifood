using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        //Endpoints para registrar segun el rol

        [HttpPost("register/estudiante")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                var token = await _authService.RegisterEstudiante(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register/vendor")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RegisterVendor([FromBody] RegisterVendorDTO dto)
        {
            try
            {
                var token = await _authService.RegisterVendor(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register/admin")]
        [AllowAnonymous]
        //Lo ideal seria tener un admin predeterminado pero por ahora puede seer asi para probar
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO dto)
        {
            try
            {
                var token = await _authService.RegisterAdmin(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //El Login se hace igual independiente del rol
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.Login(dto.Email, dto.Password);
            if (token == null) return Unauthorized(new { message = "Credenciales incorrectas" });
            return Ok(new { Token = token });
        }
    }
}