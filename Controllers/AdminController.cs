using BackendPolifood.Interface;
using BackendPolifood.Models.Users;
using BackendPolifood.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly IAdminService _IAdminService;

        public AdminController(IAdminService adminService)
        {
            _IAdminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IAdminService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IAdminService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Admin newAdmin)
        {
            var createdEstudiante = await _IAdminService.Create(newAdmin);
            return CreatedAtAction(nameof(GetById), new { id = newAdmin.Id }, newAdmin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Admin editAdmin)
        {
            var result = await _IAdminService.Edit(editAdmin, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _IAdminService.ChangeStatus(id);
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
