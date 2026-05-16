using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models;
using BackendPolifood.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StoreResponseDTO>> GetAll()
        {
            return await _context.Stores
                .Where(s => s.available == 1)
                .Select(s => MapToDTO(s))
                .ToListAsync();
        }

        public async Task<StoreResponseDTO?> GetById(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            return store != null ? MapToDTO(store) : null;
        }

        public async Task<StoreResponseDTO> Create(StoreCreateDTO dto)
        {
            var store = new Store
            {
                nombre = dto.nombre,
                categories = dto.categories,
                logoUrl = dto.logoUrl,
                available = 1
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return MapToDTO(store);
        }

        public async Task<bool> Edit(StoreUpdateDTO dto, Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return false;

            if (dto.nombre != null) store.nombre = dto.nombre;
            if (dto.categories != null) store.categories = dto.categories;
            if (dto.logoUrl != null) store.logoUrl = dto.logoUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return -1;

            store.available = store.available == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return store.available;
        }

        private static StoreResponseDTO MapToDTO(Store store) => new()
        {
            storeId = store.storeId,
            nombre = store.nombre,
            categories = store.categories,
            available = store.available,
            logoUrl = store.logoUrl
        };
    }
}
