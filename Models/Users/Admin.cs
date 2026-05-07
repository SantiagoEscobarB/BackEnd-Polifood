using BackendPolifood.Models.Enums;

namespace BackendPolifood.Models.Users
{
    public class Admin : User
    {
        public Admin(string nombre)
            : base(nombre, UserRoles.ADMIN)
        {
        }
    }
}
