using BackendPolifood.Models.DTOs;

namespace BackendPolifood.Interface
{
    public interface IAdminService
    {
        Task<List<UserResponseDTO>> GetAll();
        Task<UserResponseDTO?> GetById(string id);
        Task<UserResponseDTO> Create(UserCreateDTO dto);
        Task<bool> Edit(UserUpdateDTO dto, string id);
        Task<int> ChangeStatus(string id);
    }
}
