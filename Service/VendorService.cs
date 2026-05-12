using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public VendorService(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<VendorResponseDTO>> GetAll()
        {
            return await _context.Vendors
                .Where(v => v.active == 1)
                .Select(v => MapToDTO(v))
                .ToListAsync();
        }

        public async Task<VendorResponseDTO?> GetById(string id)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.Id == id && v.active == 1);
            return vendor != null ? MapToDTO(vendor) : null;
        }

        public async Task<VendorResponseDTO> Create(VendorCreateDTO dto)
        {
            var vendor = new Vendor(dto.nombre, dto.storeId)
            {
                Email = dto.email,
                UserName = dto.email
            };

            var result = await _userManager.CreateAsync(vendor, dto.password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            const string role = "VENDOR";
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(vendor, role);
            return MapToDTO(vendor);
        }

        public async Task<bool> Edit(VendorUpdateDTO dto, string id)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.Id == id && v.active == 1);
            if (vendor == null) return false;

            if (dto.nombre != null) vendor.nombre = dto.nombre;
            if (dto.email != null)
            {
                vendor.Email = dto.email;
                vendor.UserName = dto.email;
            }
            if (dto.storeId != null) vendor.storeId = dto.storeId.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(string id)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vendor == null) return -1;

            vendor.active = vendor.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return vendor.active;
        }

        private static VendorResponseDTO MapToDTO(Vendor vendor) => new()
        {
            id = vendor.Id,
            nombre = vendor.nombre,
            email = vendor.Email,
            userRole = vendor.userRole.ToString(),
            storeId = vendor.storeId
        };
    }
}
