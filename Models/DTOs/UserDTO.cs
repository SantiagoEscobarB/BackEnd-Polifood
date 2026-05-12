using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Models.DTOs
{
    public class UserResponseDTO
    {
        public string id { get; set; } = string.Empty;

        public string nombre { get; set; } = string.Empty;

        public string? email { get; set; }

        public string userRole { get; set; } = string.Empty;
    }

    public class VendorResponseDTO : UserResponseDTO
    {
        public Guid storeId { get; set; }
    }

    public class UserCreateDTO
    {
        [Required]
        public string nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        [MinLength(12)]
        public string password { get; set; } = string.Empty;
    }

    public class VendorCreateDTO : UserCreateDTO
    {
        [Required]
        public Guid storeId { get; set; }
    }

    public class UserUpdateDTO
    {
        public string? nombre { get; set; }

        [EmailAddress]
        public string? email { get; set; }
    }

    public class VendorUpdateDTO : UserUpdateDTO
    {
        public Guid? storeId { get; set; }
    }
}
