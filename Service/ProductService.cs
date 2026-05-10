using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Where(p => p.isActive == 1)
                .ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.productId == id && p.isActive == 1);
        }

        public async Task<List<Product>> GetByStoreId(Guid storeId)
        {
            return await _context.Products
                .Where(p => p.storeId == storeId && p.isActive == 1)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.category == category && p.isActive == 1)
                .ToListAsync();
        }

        public async Task<Product> Create(Product newProduct)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<bool> Update(Guid id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null || existingProduct.isActive == 0)
            {
                return false;
            }

            existingProduct.name = product.name;
            existingProduct.description = product.description;
            existingProduct.price = product.price;
            existingProduct.imageUrl = product.imageUrl;
            existingProduct.category = product.category;
            existingProduct.storeId = product.storeId;
            existingProduct.isAvailable = product.isAvailable;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeAvailability(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.isActive == 0)
            {
                return false;
            }

            product.isAvailable = !product.isAvailable;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            product.isActive = 0;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}