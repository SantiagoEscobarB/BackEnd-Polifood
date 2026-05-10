using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPolifood.Models.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid productId { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }

        [Required]
        public string imageUrl { get; set; } = string.Empty;

        [Required]
        public string category { get; set; } = string.Empty;

        [Required]
        public Guid storeId { get; set; }

        public bool isAvailable { get; set; } = true;

        public int isActive { get; set; } = 1;
    }
}