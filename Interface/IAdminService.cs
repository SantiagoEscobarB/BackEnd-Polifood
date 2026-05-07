using BackendPolifood.Models;
using BackendPolifood.Models.Users;

namespace BackendPolifood.Interface
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAll();
        Task<Admin> GetById(Guid id);
        Task<Admin> Create(Admin admin);
        Task<bool> Edit(Admin admin, Guid id);
        Task<int> ChangeStatus(Guid id);
    }
}
