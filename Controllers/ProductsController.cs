using BackendPolifood.Interface;
using BackendPolifood.Models.Products;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IProductService.GetAll());
        }

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

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId)
        {
            return Ok(await _IProductService.GetByStoreId(storeId));
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            return Ok(await _IProductService.GetByCategory(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product newProduct)
        {
            var createdProduct = await _IProductService.Create(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.productId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Product product)
        {
            var result = await _IProductService.Update(id, product);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeAvailability(Guid id)
        {
            var result = await _IProductService.ChangeAvailability(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

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