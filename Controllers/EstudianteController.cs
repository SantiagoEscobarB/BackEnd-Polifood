using BackendPolifood.Interface;
using BackendPolifood.Models;
using BackendPolifood.Models.Users;
using BackendPolifood.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : Controller
    {
        private readonly IEstudianteService _IEstudianteService;

        public EstudianteController(IEstudianteService estudianteService)
        {
            _IEstudianteService = estudianteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IEstudianteService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IEstudianteService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Estudiante newEstudiante)
        {
            var createdEstudiante = await _IEstudianteService.Create(newEstudiante);
            return CreatedAtAction(nameof(GetById), new { id = newEstudiante.userId }, newEstudiante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Estudiante editStore)
        {
            var result = await _IEstudianteService.Edit(editStore, id);
            return result ? Ok(true) : NotFound(false);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
