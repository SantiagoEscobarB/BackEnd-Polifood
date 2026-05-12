using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Products;

namespace BackendPolifood.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDTO>> GetAll();

        Task<ProductResponseDTO?> GetById(Guid id);

        Task<List<ProductResponseDTO>> GetByStoreId(Guid storeId);

        Task<List<ProductResponseDTO>> GetByCategory(string category);

        Task<ProductResponseDTO> Create(ProductCreateDTO dto);

        Task<bool> Update(Guid id, ProductUpdateDTO dto);

        Task<bool> ToggleAvailability(Guid id);

        Task<bool> Delete(Guid id);
    }
}