using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;
        public VendorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Vendor>> GetAll()
        {
            return await _context.Vendors.Where(v => v.active == 1).ToListAsync(); ;
        }
        public async Task<Vendor> GetById(Guid id)
        {
            return await _context.Vendors.FindAsync(id);
        }
        public async Task<Vendor> Create(Vendor vendor)
        {
            vendor.userRole = Models.Enums.UserRoles.VENDOR;
            vendor.active = 1;
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task<bool> Edit(Vendor vendor, Guid id)
        {
            var result = await _context.Vendors.FindAsync(id);
            if (result == null) return false;

            result.nombre = vendor.nombre;
            result.email = vendor.email;
            result.password = vendor.password;
            result.storeId = vendor.storeId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(Guid id)
        {
            var result = await _context.Vendors.FindAsync(id);
            if (result == null) return -1;
            result.active = result.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return result.active;
        }
    }
}
