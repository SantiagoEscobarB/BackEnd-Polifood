using BackendPolifood.Models.DTOs;

namespace BackendPolifood.Interface
{
    public interface IVendorService
    {
        Task<List<VendorResponseDTO>> GetAll();
        Task<VendorResponseDTO?> GetById(string id);
        Task<VendorResponseDTO> Create(VendorCreateDTO dto);
        Task<bool> Edit(VendorUpdateDTO dto, string id);
        Task<int> ChangeStatus(string id);
    }
}
