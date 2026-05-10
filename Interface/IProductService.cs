using BackendPolifood.Models.Products;

namespace BackendPolifood.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<List<Product>> GetByStoreId(Guid storeId);
        Task<List<Product>> GetByCategory(string category);
        Task<Product> Create(Product newProduct);
        Task<bool> Update(Guid id, Product product);
        Task<bool> ChangeAvailability(Guid id);
        Task<bool> Delete(Guid id);
    }
}