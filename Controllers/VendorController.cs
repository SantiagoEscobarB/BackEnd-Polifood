using BackendPolifood.Interface;
using BackendPolifood.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {
        private readonly IVendorService _IVendorService;

        public VendorController(IVendorService IVendorService)
        {
            _IVendorService = IVendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IVendorService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IVendorService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vendor newVendor)
        {
            var createdEstudiante = await _IVendorService.Create(newVendor);
            return CreatedAtAction(nameof(GetById), new { id = newVendor.userId }, newVendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Vendor editVendor)
        {
            var result = await _IVendorService.Edit(editVendor, id);
            return result ? Ok(true) : NotFound(false);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
