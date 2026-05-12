using BackendPolifood.Models.DTOs;

namespace BackendPolifood.Interface
{
    public interface IStoreService
    {
        Task<List<StoreResponseDTO>> GetAll();
        Task<StoreResponseDTO?> GetById(Guid id);
        Task<StoreResponseDTO> Create(StoreCreateDTO dto);
        Task<bool> Edit(StoreUpdateDTO dto, Guid id);
        Task<int> ChangeStatus(Guid id);
    }
}
