using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Models.DTOs
{
    public class RegisterVendorDTO : RegisterDTO
    {
        [Required]
        public Guid storeId { get; set; }
    }
}