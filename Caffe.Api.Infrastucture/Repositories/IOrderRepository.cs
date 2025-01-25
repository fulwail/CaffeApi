using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;

namespace Caffe.Api.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<Guid> AddOrder(Order order);
        Task<IReadOnlyCollection<MenuProduct>> ChangeProductMenuList(Guid orderId, Guid[] menuProductIds);
        Task ChangeStatus(Guid id, OrderStatusType status);
        Task DeleteOrder(Guid id);
        Task<IReadOnlyCollection<Order>> GetOrders(DateTime dateBegin, DateTime dateEnd, OrderStatusType status);
        Task<bool> IsTerminalStatus(Guid id);
    }
}