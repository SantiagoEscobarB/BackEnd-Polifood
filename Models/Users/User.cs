using BackendPolifood.Models.Enums;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPolifood.Models.Users
{
    public abstract class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid userId { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public UserRoles userRole { get; set; }
        [Required]
        [EmailAddress]
        public string email {  get; set; }
        [Required]
        [MinLength(12)]
        public string password { get; set; }
        [Required]
        public int active { get; set; } = 1;

        protected User(string name, string pass, string email, UserRoles rol, int active)
        {
            this.email = email;
            this.nombre = name;
            this.password = pass;
            this.userRole = rol;
            this.active = active;
        }
    }
}
