using BackendPolifood.DAO;
using BackendPolifood.Interface;
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

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders
                .Include(o => o.store)
                .Include(o => o.student)
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.isActive == 1)
                .ToListAsync();
        }

        public async Task<Order?> GetById(Guid id)
        {
            return await _context.Orders
                .Include(o => o.store)
                .Include(o => o.student)
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .FirstOrDefaultAsync(o => o.orderId == id && o.isActive == 1);
        }

        public async Task<List<Order>> GetByStudentId(string studentId)
        {
            return await _context.Orders
                .Include(o => o.store)
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.studentId == studentId && o.isActive == 1)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByStoreId(Guid storeId)
        {
            return await _context.Orders
                .Include(o => o.student)
                .Include(o => o.items)
                    .ThenInclude(i => i.product)
                .Where(o => o.storeId == storeId && o.isActive == 1)
                .ToListAsync();
        }

        public async Task<Order> Create(Order newOrder)
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

            newOrder.orderId = Guid.NewGuid();
            newOrder.createdAt = DateTime.Now;
            newOrder.status = OrderStatus.RECIBIDO;
            newOrder.isActive = 1;
            newOrder.total = 0;

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

                item.orderItemId = Guid.NewGuid();
                item.orderId = newOrder.orderId;
                item.productName = product.name;
                item.price = product.price;

                newOrder.total += product.price * item.quantity;
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return await GetById(newOrder.orderId) ?? newOrder;
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
    }
}