using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderResponseDTO>> GetAll()
        {
            return await _context.Orders
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.isActive == 1)
                .Select(o => MapToDTO(o))
                .ToListAsync();
        }

        public async Task<OrderResponseDTO?> GetById(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .FirstOrDefaultAsync(o => o.orderId == id && o.isActive == 1);

            if (order == null)
            {
                return null;
            }

            return MapToDTO(order);
        }

        public async Task<List<OrderResponseDTO>> GetByStudentId(string studentId)
        {
            return await _context.Orders
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.studentId == studentId && o.isActive == 1)
                .Select(o => MapToDTO(o))
                .ToListAsync();
        }

        public async Task<List<OrderResponseDTO>> GetByStoreId(Guid storeId)
        {
            return await _context.Orders
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.storeId == storeId && o.isActive == 1)
                .Select(o => MapToDTO(o))
                .ToListAsync();
        }

        public async Task<OrderResponseDTO> Create(OrderCreateDTO newOrder)
        {
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == newOrder.studentId && u.active == 1);

            if (student == null)
            {
                throw new Exception("El estudiante no existe o no está activo");
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.storeId == newOrder.storeId && s.available == 1);

            if (store == null)
            {
                throw new Exception("La tienda no existe o no está disponible");
            }

            if (newOrder.items == null || newOrder.items.Count == 0)
            {
                throw new Exception("La orden debe tener al menos un producto");
            }

            var order = new Order
            {
                orderId = Guid.NewGuid(),
                studentId = newOrder.studentId,
                storeId = newOrder.storeId,
                createdAt = DateTime.Now,
                status = OrderStatus.RECIBIDO,
                isActive = 1,
                total = 0,
                items = new List<OrderItem>()
            };

            foreach (var item in newOrder.items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p =>
                        p.productId == item.productId &&
                        p.storeId == newOrder.storeId &&
                        p.isActive == 1 &&
                        p.isAvailable == true);

                if (product == null)
                {
                    throw new Exception("El producto no existe, no pertenece a la tienda o no está disponible");
                }

                if (item.quantity <= 0)
                {
                    throw new Exception("La cantidad del producto debe ser mayor a cero");
                }

                var orderItem = new OrderItem
                {
                    orderItemId = Guid.NewGuid(),
                    orderId = order.orderId,
                    productId = product.productId,
                    productName = product.name,
                    quantity = item.quantity,
                    price = product.price
                };

                order.items.Add(orderItem);
                order.total += product.price * item.quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var createdOrder = await _context.Orders
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .FirstOrDefaultAsync(o => o.orderId == order.orderId);

            return MapToDTO(createdOrder!);
        }

        public async Task<bool> ChangeStatus(Guid id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null || order.isActive == 0)
            {
                return false;
            }

            order.status = status;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            order.isActive = 0;
            await _context.SaveChangesAsync();

            return true;
        }

        private static OrderResponseDTO MapToDTO(Order order)
        {
            return new OrderResponseDTO
            {
                orderId = order.orderId,
                studentId = order.studentId,
                storeId = order.storeId,
                total = order.total,
                etaMinutes = order.etaMinutes,
                status = order.status.ToString(),
                createdAt = order.createdAt,
                items = order.items.Select(i => new OrderItemResponseDTO
                {
                    productId = i.productId,
                    productName = i.productName,
                    quantity = i.quantity,
                    price = i.price
                }).ToList()
            };
        }
    }
}