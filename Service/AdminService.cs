using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Admin>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }
        public async Task<Admin> GetById(Guid id)
        {
            return await _context.Admins.FindAsync(id);
        }
        public async Task<Admin> Create(Admin admin)
        {
            admin.userRole = Models.Enums.UserRoles.ADMIN;
            admin.active = 1;
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> Edit(Admin admin, Guid id)
        {
            var result = await _context.Admins.FindAsync(id);
            if (result == null) return false;

            result.nombre = admin.nombre;
            result.email = admin.email;
            result.password = admin.password;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(Guid id)
        {
            var result = await _context.Admins.FindAsync(id);
            if (result == null) return -1;
            result.active = result.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return result.active;
        }
    }
}

