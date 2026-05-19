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
    public class StoresController : Controller
    {
        private readonly IStoreService _IStoreService;

        public StoresController(IStoreService storeService)
        {
            _IStoreService = storeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IStoreService.GetAll());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IStoreService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] StoreCreateDTO dto)
        {
            var created = await _IStoreService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.storeId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] StoreUpdateDTO dto)
        {
            if (User.IsInRole("VENDOR"))
            {
                var storeIdClaim = User.FindFirstValue("storeId");
                if (!Guid.TryParse(storeIdClaim, out var vendorStoreId) || vendorStoreId != id)
                    return Forbid();
            }

            var result = await _IStoreService.Edit(dto, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            if (User.IsInRole("VENDOR"))
            {
                var storeIdClaim = User.FindFirstValue("storeId");
                if (!Guid.TryParse(storeIdClaim, out var vendorStoreId) || vendorStoreId != id)
                    return Forbid();
            }

            var result = await _IStoreService.ChangeStatus(id);
            if (result == -1) return NotFound();
            var isAvailable = result == 1 ? "Available" : "Not Available";
            return Ok(isAvailable);
        }
    }
}
