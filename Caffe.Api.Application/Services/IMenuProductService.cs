using Caffe.Api.Application.Dtos.MenuProduct;

namespace Caffe.Api.Application.Services
{
    public interface IMenuProductService
    {
        Task<Guid> CreateMenuProduct(CreateOrUpdateMenuProduct createItem);
        Task DeleteMenuProduct(Guid id);
        Task<IReadOnlyCollection<MenuProductDto>> GetMenus();
        Task<bool> IsDuplicateName(string name,Guid? id=null);
        Task RestoreMenuProduct(Guid id);
    }
}