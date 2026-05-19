using System.ComponentModel.DataAnnotations;
using BackendPolifood.Models.Orders;

namespace BackendPolifood.Models.DTOs
{
    public class OrderItemCreateDTO
    {
        [Required]
        public Guid productId { get; set; }

        [Required]
        public int quantity { get; set; }
    }

    public class OrderCreateDTO
    {
        [Required]
        public string studentId { get; set; } = string.Empty;

        [Required]
        public Guid storeId { get; set; }

        [Required]
        public List<OrderItemCreateDTO> items { get; set; } = new();
    }

    public class OrderStatusUpdateDTO
    {
        [Required]
        public OrderStatus status { get; set; }
    }

    public class OrderItemResponseDTO
    {
        public Guid productId { get; set; }

        public string productName { get; set; } = string.Empty;

        public int quantity { get; set; }

        public decimal price { get; set; }
    }

    public class OrderResponseDTO
    {
        public Guid orderId { get; set; }

        public string studentId { get; set; } = string.Empty;

        public string studentName { get; set; } = string.Empty;

        public string studentEmail { get; set; } = string.Empty;

        public Guid storeId { get; set; }

        public decimal total { get; set; }

        public int etaMinutes { get; set; }

        public string status { get; set; } = string.Empty;

        public DateTime createdAt { get; set; }

        public List<OrderItemResponseDTO> items { get; set; } = new();
    }
}