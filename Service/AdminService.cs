using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<UserResponseDTO>> GetAll()
        {
            return await _context.Admins
                .Where(a => a.active == 1)
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task<UserResponseDTO?> GetById(string id)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.active == 1);
            return admin != null ? MapToDTO(admin) : null;
        }

        public async Task<UserResponseDTO> Create(UserCreateDTO dto)
        {
            var admin = new Admin(dto.nombre)
            {
                Email = dto.email,
                UserName = dto.email
            };

            var result = await _userManager.CreateAsync(admin, dto.password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            const string role = "ADMIN";
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(admin, role);
            return MapToDTO(admin);
        }

        public async Task<bool> Edit(UserUpdateDTO dto, string id)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.active == 1);
            if (admin == null) return false;

            if (dto.nombre != null) admin.nombre = dto.nombre;
            if (dto.email != null)
            {
                admin.Email = dto.email;
                admin.UserName = dto.email;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(string id)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id);
            if (admin == null) return -1;

            admin.active = admin.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return admin.active;
        }

        private static UserResponseDTO MapToDTO(Admin admin) => new()
        {
            id = admin.Id,
            nombre = admin.nombre,
            email = admin.Email,
            userRole = admin.userRole.ToString()
        };
    }
}
