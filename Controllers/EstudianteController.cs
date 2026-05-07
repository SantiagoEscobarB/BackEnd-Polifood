using BackendPolifood.Interface;
using BackendPolifood.Models;
using BackendPolifood.Models.Users;
using BackendPolifood.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstudianteController : Controller
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IEstudianteService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] Estudiante newEstudiante)
        {
            var createdEstudiante = await _IEstudianteService.Create(newEstudiante);
            return CreatedAtAction(nameof(GetById), new { id = newEstudiante.Id }, newEstudiante);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,ESTUDIANTE")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Estudiante editStore)
        {
            var result = await _IEstudianteService.Edit(editStore, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _IEstudianteService.ChangeStatus(id);
            if (result == -1) return NotFound(id);
            var isActive = result == 1 ? "Active" : "Not Active";
            return Ok(isActive);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
