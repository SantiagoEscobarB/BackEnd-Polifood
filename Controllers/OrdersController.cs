using BackendPolifood.Interface;
using BackendPolifood.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _IOrderService;

        public OrdersController(IOrderService orderService)
        {
            _IOrderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IOrderService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _IOrderService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(string studentId)
        {
            return Ok(await _IOrderService.GetByStudentId(studentId));
        }

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId)
        {
            return Ok(await _IOrderService.GetByStoreId(storeId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order newOrder)
        {
            var createdOrder = await _IOrderService.Create(newOrder);

            return CreatedAtAction(nameof(GetById), new { id = createdOrder.orderId }, createdOrder);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] OrderStatus status)
        {
            var result = await _IOrderService.ChangeStatus(id, status);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _IOrderService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}