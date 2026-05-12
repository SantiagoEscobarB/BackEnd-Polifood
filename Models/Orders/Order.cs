using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendPolifood.Models.Users;

namespace BackendPolifood.Models.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid orderId { get; set; }

        [Required]
        public string studentId { get; set; } = string.Empty;

        [ForeignKey("studentId")]
        public User? student { get; set; }

        [Required]
        public Guid storeId { get; set; }

        [ForeignKey("storeId")]
        public Store? store { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }

        public int etaMinutes { get; set; }

        public OrderStatus status { get; set; } = OrderStatus.RECIBIDO;

        public DateTime createdAt { get; set; } = DateTime.Now;

        public int isActive { get; set; } = 1;

        public List<OrderItem> items { get; set; } = new();
    }
}