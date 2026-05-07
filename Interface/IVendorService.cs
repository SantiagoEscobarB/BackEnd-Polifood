using BackendPolifood.Models.Users;

namespace BackendPolifood.Interface
{
    public interface IVendorService
    {
        Task<List<Vendor>> GetAll();
        Task<Vendor> GetById(Guid id);
        Task<Vendor> Create(Vendor vendor);
        Task<bool> Edit(Vendor vendor, Guid id);
        Task<int> ChangeStatus(Guid id);
    }
}
