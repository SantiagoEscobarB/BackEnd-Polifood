using BackendPolifood.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPolifood.Models.Users
{
    public abstract class User : IdentityUser
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public UserRoles userRole { get; set; }
        public int active { get; set; } = 1;

        protected User(string nombre, UserRoles rol)
        {
            this.nombre = nombre;
            this.userRole = rol;
            this.UserName = nombre;
        }
    }
}
