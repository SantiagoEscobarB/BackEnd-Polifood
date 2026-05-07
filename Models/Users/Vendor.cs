using BackendPolifood.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPolifood.Models.Users
{
    public class Vendor : User
    {
        [Required]
        public Guid storeId { get; set; }
        [ForeignKey("storeId")]
        public Store? Store { get; set; }

        public Vendor(string nombre, string password, string email, Guid storeId, int active)
            : base(nombre, password, email, UserRoles.VENDOR, active)
        {
            this.storeId = storeId;
        }
    }
}
