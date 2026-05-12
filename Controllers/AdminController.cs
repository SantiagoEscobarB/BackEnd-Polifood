using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class AdminController : ControllerBase
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
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _IAdminService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO dto)
        {
            try
            {
                var created = await _IAdminService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] UserUpdateDTO dto)
        {
            var result = await _IAdminService.Edit(dto, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var result = await _IAdminService.ChangeStatus(id);
            if (result == -1) return NotFound();
            var isActive = result == 1 ? "Active" : "Not Active";
            return Ok(isActive);
        }
    }
}
