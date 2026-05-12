using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _IProductService;

        public ProductsController(IProductService productService)
        {
            _IProductService = productService;
        }

        [Authorize(Roles = "ADMIN,VENDOR,ESTUDIANTE")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IProductService.GetAll());
        }

        [Authorize(Roles = "ADMIN,VENDOR,ESTUDIANTE")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IProductService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN,VENDOR,ESTUDIANTE")]
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId)
        {
            return Ok(await _IProductService.GetByStoreId(storeId));
        }

        [Authorize(Roles = "ADMIN,VENDOR,ESTUDIANTE")]
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            return Ok(await _IProductService.GetByCategory(category));
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO newProduct)
        {
            var createdProduct = await _IProductService.Create(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.productId }, createdProduct);
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDTO product)
        {
            var result = await _IProductService.Update(id, product);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeAvailability(Guid id)
        {
            var result = await _IProductService.ToggleAvailability(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _IProductService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}