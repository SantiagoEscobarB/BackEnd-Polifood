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
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _IVendorService;

        public VendorController(IVendorService IVendorService)
        {
            _IVendorService = IVendorService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IVendorService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _IVendorService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] VendorCreateDTO dto)
        {
            try
            {
                var created = await _IVendorService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> Edit(string id, [FromBody] VendorUpdateDTO dto)
        {
            if (User.IsInRole("VENDOR"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != id)
                    return Forbid();
            }

            var result = await _IVendorService.Edit(dto, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var result = await _IVendorService.ChangeStatus(id);
            if (result == -1) return NotFound();
            var isActive = result == 1 ? "Active" : "Not Active";
            return Ok(isActive);
        }
    }
}
