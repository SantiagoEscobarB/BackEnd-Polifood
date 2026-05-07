using BackendPolifood.DAO;
using BackendPolifood.Interface;
using BackendPolifood.Models;
using BackendPolifood.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.Service
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;
        public EstudianteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Estudiante>> GetAll()
        {
            return await _context.Estudiantes.Where(e => e.active == 1).ToListAsync(); ;
        }
        public async Task<Estudiante> GetById(Guid id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }
        public async Task<Estudiante> Create(Estudiante estudiante)
        {
            estudiante.userRole = Models.Enums.UserRoles.ESTUDIANTE;
            estudiante.active = 1;
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<bool> Edit(Estudiante estudiante, Guid id)
        {
            var result = await _context.Estudiantes.FindAsync(id);
            if (result == null) return false;

            result.nombre = estudiante.nombre;
            result.email = estudiante.email;
            result.password = estudiante.password;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> ChangeStatus(Guid id)
        {
            var result = await _context.Estudiantes.FindAsync(id);
            if (result == null) return -1;
            result.active = result.active == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            return result.active;
        }
    }
}
