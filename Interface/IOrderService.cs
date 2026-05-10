using BackendPolifood.Models;
using BackendPolifood.Models.Orders;

namespace BackendPolifood.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<Order?> GetById(Guid id);
        Task<List<Order>> GetByStudentId(string studentId);
        Task<List<Order>> GetByStoreId(Guid storeId);
        Task<Order> Create(Order newOrder);
        Task<bool> ChangeStatus(Guid id, OrderStatus status);
        Task<bool> Delete(Guid id);
    }
}