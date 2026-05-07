using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models;  
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
        public Task<List<Store>> GetAll()
        {
            return _context.Stores.Where(s => s.available == 1).ToListAsync();
        }
        public async Task<Store> GetById(Guid id)
        {
            return await _context.Stores.FindAsync(id);
        }
        public async Task<Store> Create(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<bool> Edit(Store store, Guid id)
        {
            var result = await _context.Stores.FindAsync(id);
            if (result == null) return false;

            result.nombre = store.nombre;
            result.categories = store.categories;
            result.available = store.available;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> ChangeStatus(Guid id)
        {
            var result = await _context.Stores.FindAsync(id);
            if (result == null) return -1;
            result.available = result.available == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return result.available;
        }
    }
}
