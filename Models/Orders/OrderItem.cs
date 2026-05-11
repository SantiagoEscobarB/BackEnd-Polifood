using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackendPolifood.Models.Products;

namespace BackendPolifood.Models.Orders
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid orderItemId { get; set; }

        [Required]
        public Guid orderId { get; set; }

        [JsonIgnore]
        public Order? order { get; set; }

        [Required]
        public Guid productId { get; set; }

        [ForeignKey("productId")]
        public Product? product { get; set; }

        [Required]
        public string productName { get; set; } = string.Empty;

        [Required]
        public int quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }
    }
}