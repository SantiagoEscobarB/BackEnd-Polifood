using BackendPolifood.Interface;
using BackendPolifood.Models.Users;
using BackendPolifood.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VendorController : Controller
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IVendorService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] Vendor newVendor)
        {
            var createdEstudiante = await _IVendorService.Create(newVendor);
            return CreatedAtAction(nameof(GetById), new { id = newVendor.Id }, newVendor);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Vendor editVendor)
        {
            var result = await _IVendorService.Edit(editVendor, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _IVendorService.ChangeStatus(id);
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
