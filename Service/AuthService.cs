using BackendPolifood.Interface;
using BackendPolifood.Models;
using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Enums;
using BackendPolifood.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendPolifood.Service
{
    public class AuthService : IAuthService
    {   
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<string?> RegisterEstudiante(RegisterDTO dto)
        {
            var estudiante = new Estudiante(dto.nombre);
            estudiante.Email = dto.email;
            return await CrearUsuario(estudiante, dto.password, estudiante.userRole.ToString());
        }

        public async Task<string?> RegisterVendor(RegisterVendorDTO dto)
        {
            var vendor = new Vendor(dto.nombre, dto.storeId);
            vendor.Email = dto.email;
            return await CrearUsuario(vendor, dto.password, vendor.userRole.ToString());
        }

        public async Task<string?> RegisterAdmin(RegisterDTO dto)
        {
            var admin = new Admin(dto.nombre);
            admin.Email = dto.email;
            return await CrearUsuario(admin, dto.password, admin.userRole.ToString());
        }

        private async Task<string?> CrearUsuario(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
            return GenerarToken(user, role);
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            if (!await _userManager.CheckPasswordAsync(user, password)) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return GenerarToken(user, roles.FirstOrDefault() ?? "");
        }

        private string GenerarToken(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user is Vendor vendor)
                claims.Add(new Claim("storeId", vendor.storeId.ToString()));


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims.ToArray(),
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}