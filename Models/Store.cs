using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public int available { get; set; } = 1;

    }
}
