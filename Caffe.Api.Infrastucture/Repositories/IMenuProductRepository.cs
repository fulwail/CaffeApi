using Caffe.Api.Domain.Models;

namespace Caffe.Api.Infrastructure.Repositories
{
    public interface IMenuProductRepository
    {
        Task<Guid> CreateMenuProduct(string name);
        Task DeleteMenuProduct(Guid id);
        Task<IReadOnlyCollection<MenuProduct>> GetMenus(bool showDeleted);
        Task<bool> IsDuplicateName(string name,Guid? id=null);
        Task RestoreMenuProduct(Guid id);
        Task<MenuProduct> UpdateMenuProduct(string name, Guid id);
    }
}