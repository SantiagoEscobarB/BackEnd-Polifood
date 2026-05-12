using BackendPolifood.Models.DTOs;
using BackendPolifood.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BackendPolifood.Interface
    {
    public interface IAuthService
    {
        Task<string?> RegisterEstudiante(RegisterDTO dto);
        Task<string?> RegisterVendor(RegisterVendorDTO dto);
        Task<string?> RegisterAdmin(RegisterDTO dto);
        Task<string?> Login(string email, string password);
    }
}