using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Application.Dtos.Order;
using Caffe.Api.Domain.Enums;

namespace Caffe.Api.Application.Services
{
    public interface IOrderService
    {
        Task<IReadOnlyCollection<MenuProductDto>> ChangeProductMenuList(Guid orderId, Guid[] menuProductIds);
        Task ChangeStatus(Guid id, OrderStatusType status);
        Task<Guid> CreateOrder(CreateOrder createOrderDto);
        Task DeleteOrder(Guid id);
        Task<IReadOnlyCollection<OrderDto>> GetOrders(DateTime dateBegin, DateTime dateEnd, OrderStatusType status);
        Task<bool> IsTerminalStatus(Guid id);
    }
}