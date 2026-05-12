using System.ComponentModel.DataAnnotations;

namespace BackendPolifood.Models.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public decimal price { get; set; }

        [Required]
        public string imageUrl { get; set; }

        [Required]
        public string category { get; set; }

        [Required]
        public Guid storeId { get; set; }

        public bool isAvailable { get; set; } = true;
    }

    public class ProductUpdateDTO
    {
        public string? name { get; set; }

        public string? description { get; set; }

        public decimal? price { get; set; }

        public string? imageUrl { get; set; }

        public string? category { get; set; }

        public bool? isAvailable { get; set; }
    }

    public class ProductResponseDTO
    {
        public Guid productId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public string imageUrl { get; set; }

        public string category { get; set; }

        public Guid storeId { get; set; }

        public bool isAvailable { get; set; }

        public int isActive { get; set; }
    }
}