using BackendPolifood.Models;
using BackendPolifood.Models.Users;

namespace BackendPolifood.Interface
{
    public interface IEstudianteService
    {
        Task<List<Estudiante>> GetAll();
        Task<Estudiante> GetById(Guid id);
        Task<Estudiante> Create(Estudiante estudiante);
        Task<bool> Edit(Estudiante estudiante, Guid id);
        Task<int> ChangeStatus(Guid id);
    }
}
