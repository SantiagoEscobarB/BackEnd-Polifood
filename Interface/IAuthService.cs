using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BackendPolifood.Interface
    {
    public interface IAuthService
    {
        Task<AuthResponseDTO?> RegisterEstudiante(RegisterDTO dto);
        Task<string?> RegisterVendor(RegisterVendorDTO dto);
        Task<string?> RegisterAdmin(RegisterDTO dto);
        Task<AuthResponseDTO?> Login(string email, string password);
    }
}