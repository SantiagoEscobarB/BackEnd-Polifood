using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models;
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
                .Include(o => o.items)
                .Where(o => o.isActive == 1)
                .ToListAsync();
        }

        public async Task<Order?> GetById(Guid id)
        {
            return await _context.Orders
                .Include(o => o.items)
                .FirstOrDefaultAsync(o => o.orderId == id && o.isActive == 1);
        }

        public async Task<List<Order>> GetByStudentId(string studentId)
        {
            return await _context.Orders
                .Include(o => o.items)
                .Where(o => o.studentId == studentId && o.isActive == 1)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByStoreId(Guid storeId)
        {
            return await _context.Orders
                .Include(o => o.items)
                .Where(o => o.storeId == storeId && o.isActive == 1)
                .ToListAsync();
        }

        public async Task<Order> Create(Order newOrder)
        {
            newOrder.orderId = Guid.NewGuid();
            newOrder.createdAt = DateTime.Now;
            newOrder.status = OrderStatus.RECIBIDO;
            newOrder.isActive = 1;

            if (newOrder.items != null)
            {
                foreach (var item in newOrder.items)
                {
                    item.orderItemId = Guid.NewGuid();
                    item.orderId = newOrder.orderId;
                }

                newOrder.total = newOrder.items.Sum(i => i.price * i.quantity);
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
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