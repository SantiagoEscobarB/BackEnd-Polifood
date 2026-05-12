using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(12)]
        public string password { get; set; }
    }
}