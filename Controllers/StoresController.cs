using BackendPolifood.Interface;
using BackendPolifood.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : Controller
    {
        private readonly IStoreService _IStoreServices;

        public StoresController(IStoreService storeService)
        {
            _IStoreServices = storeService;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IStoreServices.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IStoreServices.GetById(id);
            return result != null ? Ok(result) : NotFound(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Store newStore)
        {
            var createdStore = await _IStoreServices.Create(newStore);
            return CreatedAtAction(nameof(GetById), new { id = newStore.storeId }, newStore);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Store editStore)
        {
            var result = await _IStoreServices.Edit(editStore, id);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _IStoreServices.ChangeStatus(id);
            var isAvailable = result == 1 ? "Available" : "Not Available";
            return Ok(isAvailable);
        }
       
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
