using BackendPolifood.Models.Enums;

namespace BackendPolifood.Models.Users
{
    public class Estudiante : User
    {
        public Estudiante(string nombre, string password, string email, int active)
            : base(nombre, password, email, UserRoles.ESTUDIANTE, active)
        {
        }
    }
}