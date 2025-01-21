using Caffe.Api.Domain.Models;

namespace Caffe.Api.Domain.Repositories
{
    public interface IMenuProductRepository
    {
        Task<Guid> CreateMenuProduct(string name);
        Task DeleteMenuProduct(Guid id);
        Task<IReadOnlyCollection<MenuProduct>> GetMenus();
        Task<bool> IsDuplicateName(string name,Guid? id=null);
        Task RestoreMenuProduct(Guid id);
    }
}