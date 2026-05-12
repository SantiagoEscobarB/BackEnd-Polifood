using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EstudianteService(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<UserResponseDTO>> GetAll()
        {
            return await _context.Estudiantes
                .Where(e => e.active == 1)
                .Select(e => MapToDTO(e))
                .ToListAsync();
        }

        public async Task<UserResponseDTO?> GetById(string id)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Id == id && e.active == 1);
            return estudiante != null ? MapToDTO(estudiante) : null;
        }

        public async Task<UserResponseDTO> Create(UserCreateDTO dto)
        {
            var estudiante = new Estudiante(dto.nombre)
            {
                Email = dto.email,
                UserName = dto.email
            };

            var result = await _userManager.CreateAsync(estudiante, dto.password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            const string role = "ESTUDIANTE";
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(estudiante, role);
            return MapToDTO(estudiante);
        }

        public async Task<bool> Edit(UserUpdateDTO dto, string id)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Id == id && e.active == 1);
            if (estudiante == null) return false;

            if (dto.nombre != null) estudiante.nombre = dto.nombre;
            if (dto.email != null)
            {
                estudiante.Email = dto.email;
                estudiante.UserName = dto.email;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ChangeStatus(string id)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Id == id);
            if (estudiante == null) return -1;

            estudiante.active = estudiante.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return estudiante.active;
        }

        private static UserResponseDTO MapToDTO(Estudiante estudiante) => new()
        {
            id = estudiante.Id,
            nombre = estudiante.nombre,
            email = estudiante.Email,
            userRole = estudiante.userRole.ToString()
        };
    }
}
