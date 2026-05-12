using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Orders;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _IOrderService.GetAll());
        }

        [Authorize(Roles = "ADMIN,VENDOR,ESTUDIANTE")]
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

        [Authorize(Roles = "ADMIN,ESTUDIANTE")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(string studentId)
        {
            return Ok(await _IOrderService.GetByStudentId(studentId));
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId)
        {
            return Ok(await _IOrderService.GetByStoreId(storeId));
        }

        [Authorize(Roles = "ESTUDIANTE")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO newOrder)
        {
            var createdOrder = await _IOrderService.Create(newOrder);

            return CreatedAtAction(nameof(GetById), new { id = createdOrder.orderId }, createdOrder);
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
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

        [Authorize(Roles = "ADMIN")]
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