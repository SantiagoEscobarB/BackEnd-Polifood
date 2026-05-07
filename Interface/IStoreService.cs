using BackendPolifood.Models;

namespace BackendPolifood.Interface
{
    public interface IStoreService
    {
        Task<List<Store>> GetAll();
        Task<Store> GetById(Guid id);
        Task<Store> Create(Store store);
        Task<bool> Edit(Store store, Guid id);
        Task<int> ChangeStatus(Guid id);
    }
}
