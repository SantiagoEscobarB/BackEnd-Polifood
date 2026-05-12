using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _IEstudianteService;

        public EstudianteController(IEstudianteService estudianteService)
        {
            _IEstudianteService = estudianteService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IEstudianteService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,ESTUDIANTE")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _IEstudianteService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO dto)
        {
            try
            {
                var created = await _IEstudianteService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,ESTUDIANTE")]
        public async Task<IActionResult> Edit(string id, [FromBody] UserUpdateDTO dto)
        {
            if (User.IsInRole("ESTUDIANTE"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != id)
                    return Forbid();
            }

            var result = await _IEstudianteService.Edit(dto, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var result = await _IEstudianteService.ChangeStatus(id);
            if (result == -1) return NotFound();
            var isActive = result == 1 ? "Active" : "Not Active";
            return Ok(isActive);
        }
    }
}
