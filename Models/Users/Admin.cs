using BackendPolifood.Models.Enums;

namespace BackendPolifood.Models.Users
{
    public class Admin : User
    {
        public Admin(string nombre, string password, string email, int active)
            : base(nombre, password, email, UserRoles.ADMIN, active)
        {
        }
    }
}
