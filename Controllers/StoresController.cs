using BackendPolifood.Interface;
using BackendPolifood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Create([FromBody] Store newStore)
        {
            var createdStore = await _IStoreService.Create(newStore);
            return CreatedAtAction(nameof(GetById), new { id = newStore.storeId }, newStore);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,VENDOR")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Store editStore)
        {
            var result = await _IStoreService.Edit(editStore, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _IStoreService.ChangeStatus(id);
            var isAvailable = result == 1 ? "Available" : "Not Available";
            return Ok(isAvailable);
        }
       
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
