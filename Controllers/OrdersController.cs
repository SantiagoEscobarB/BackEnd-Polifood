using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendPolifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
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
                return NotFound();

            return Ok(result);
        }

        // Medio 13: un Estudiante solo puede ver sus propias órdenes
        [Authorize(Roles = "ADMIN,ESTUDIANTE")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(string studentId)
        {
            if (User.IsInRole("ESTUDIANTE"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != studentId)
                    return Forbid();
            }

            return Ok(await _IOrderService.GetByStudentId(studentId));
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId)
        {
            return Ok(await _IOrderService.GetByStoreId(storeId));
        }

        // Medio 14: studentId siempre viene del token, no del body
        [Authorize(Roles = "ESTUDIANTE")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO newOrder)
        {
            newOrder.studentId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            try
            {
                var createdOrder = await _IOrderService.Create(newOrder);
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.orderId }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "ADMIN,VENDOR")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] OrderStatusUpdateDTO dto)
        {
            var result = await _IOrderService.ChangeStatus(id, dto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _IOrderService.Delete(id);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}
