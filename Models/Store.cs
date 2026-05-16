using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendPolifood.Models.Products;
using BackendPolifood.Models.Orders;

namespace BackendPolifood.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid storeId { get; set; }

        [Required]
        public string nombre { get; set; }

        public string[] categories { get; set; }

        public string logoUrl { get; set; } = string.Empty;

        [Required]
        public int available { get; set; } = 1;

        public List<Product> products { get; set; } = new();

        public List<Order> orders { get; set; } = new();
    }
}