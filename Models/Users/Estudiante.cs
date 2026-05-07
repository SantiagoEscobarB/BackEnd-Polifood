using BackendPolifood.Models.Enums;

namespace BackendPolifood.Models.Users
{
    public class Estudiante : User
    {
        public Estudiante(string nombre)
            : base(nombre, UserRoles.ESTUDIANTE)
        {
        }
    }
}