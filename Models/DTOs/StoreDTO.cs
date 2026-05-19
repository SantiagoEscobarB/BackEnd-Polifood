using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Models.DTOs
{
    public class StoreCreateDTO
    {
        [Required]
        public string nombre { get; set; } = string.Empty;

        public string[] categories { get; set; } = [];

        public string logoUrl { get; set; } = string.Empty;
    }

    public class StoreUpdateDTO
    {
        public string? nombre { get; set; }

        public string[]? categories { get; set; }

        public string? logoUrl { get; set; }
    }

    public class StoreResponseDTO
    {
        public Guid storeId { get; set; }

        public string nombre { get; set; } = string.Empty;

        public string[] categories { get; set; } = [];

        public int available { get; set; }

        public string logoUrl { get; set; } = string.Empty;
    }
}
