using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Orders;

namespace BackendPolifood.Interface
{
    public interface IOrderService
    {
        Task<List<OrderResponseDTO>> GetAll();

        Task<OrderResponseDTO?> GetById(Guid id);

        Task<List<OrderResponseDTO>> GetByStudentId(string studentId);

        Task<List<OrderResponseDTO>> GetByStoreId(Guid storeId);

        Task<OrderResponseDTO> Create(OrderCreateDTO newOrder);

        Task<bool> ChangeStatus(Guid id, OrderStatus status);

        Task<bool> Delete(Guid id);
    }
}