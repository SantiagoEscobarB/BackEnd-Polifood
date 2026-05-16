using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
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

        public async Task<List<ProductResponseDTO>> GetAll()
        {
            return await _context.Products
                .Where(p => p.isActive == 1)
                .Select(p => new ProductResponseDTO
                {
                    productId = p.productId,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    imageUrl = p.imageUrl,
                    category = p.category,
                    storeId = p.storeId,
                    isAvailable = p.isAvailable,
                    isActive = p.isActive,
                    prepTimeMinutes = p.prepTimeMinutes

                })
                .ToListAsync();
        }

        public async Task<ProductResponseDTO?> GetById(Guid id)
        {
            return await _context.Products
                .Where(p => p.productId == id && p.isActive == 1)
                .Select(p => new ProductResponseDTO
                {
                    productId = p.productId,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    imageUrl = p.imageUrl,
                    category = p.category,
                    storeId = p.storeId,
                    isAvailable = p.isAvailable,
                    isActive = p.isActive,
                    prepTimeMinutes = p.prepTimeMinutes

                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductResponseDTO>> GetByStoreId(Guid storeId)
        {
            return await _context.Products
                .Where(p => p.storeId == storeId && p.isActive == 1)
                .Select(p => new ProductResponseDTO
                {
                    productId = p.productId,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    imageUrl = p.imageUrl,
                    category = p.category,
                    storeId = p.storeId,
                    isAvailable = p.isAvailable,
                    isActive = p.isActive,
                    prepTimeMinutes = p.prepTimeMinutes
                })
                .ToListAsync();
        }

        public async Task<List<ProductResponseDTO>> GetByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.category == category && p.isActive == 1)
                .Select(p => new ProductResponseDTO
                {
                    productId = p.productId,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    imageUrl = p.imageUrl,
                    category = p.category,
                    storeId = p.storeId,
                    isAvailable = p.isAvailable,
                    isActive = p.isActive,
                    prepTimeMinutes = p.prepTimeMinutes
                })
                .ToListAsync();
        }

        public async Task<ProductResponseDTO> Create(ProductCreateDTO dto)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.storeId == dto.storeId && s.available == 1);

            if (store == null)
            {
                throw new Exception("La tienda no existe o no está disponible");
            }

            var product = new Product
            {
                name = dto.name,
                description = dto.description,
                price = dto.price,
                imageUrl = dto.imageUrl,
                category = dto.category,
                storeId = dto.storeId,
                isAvailable = dto.isAvailable,
                isActive = 1,
                prepTimeMinutes = dto.prepTimeMinutes,

            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponseDTO
            {
                productId = product.productId,
                name = product.name,
                description = product.description,
                price = product.price,
                imageUrl = product.imageUrl,
                category = product.category,
                storeId = product.storeId,
                isAvailable = product.isAvailable,
                isActive = product.isActive,
                prepTimeMinutes = product.prepTimeMinutes,

            };
        }

        public async Task<bool> Update(Guid id, ProductUpdateDTO dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.isActive == 0)
            {
                return false;
            }

            if (dto.name != null)
            {
                product.name = dto.name;
            }

            if (dto.description != null)
            {
                product.description = dto.description;
            }

            if (dto.price != null)
            {
                product.price = dto.price.Value;
            }

            if (dto.imageUrl != null)
            {
                product.imageUrl = dto.imageUrl;
            }

            if (dto.category != null)
            {
                product.category = dto.category;
            }

            if (dto.isAvailable != null)
            {
                product.isAvailable = dto.isAvailable.Value;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleAvailability(Guid id)
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