using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICaffeContext _context;

        public OrderRepository(ICaffeContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task DeleteOrder(Guid id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity != null)
            {
                _context.Orders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeStatus(Guid id, OrderStatusType status)
        {
            var entity = await _context.Orders.FindAsync(id);

            if (entity != null)
            {
                if (entity.IsTerminalStatus)
                    throw new ArgumentException("У выбранного заказа отсутствует возможность изменить статус");
                entity.Status = status;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IReadOnlyCollection<Order>> GetOrders(DateTime dateBegin, DateTime dateEnd, OrderStatusType status)
        {
            return await _context.Orders
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductMenu)
                .Where(x => dateBegin <= x.Created && x.Created <= dateEnd && status == x.Status)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<MenuProduct>> ChangeProductMenuList(Guid orderId, Guid[] menuProductIds)
        {
            var entity = await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == orderId);

            if (entity == null) return null;

            _context.OrderMenuProduct.RemoveRange(entity.Products);

            var products = await _context.MenuProducts.Where(x => menuProductIds.Contains(x.Id)).ToArrayAsync();

            var productsOrderRelation = products.Select(x => new OrderMenuProduct()
            {
                Id = Guid.NewGuid(),
                Order = entity,
                OrderId = orderId,
                ProductMenuId = x.Id,
                ProductMenu = x
            });
            await _context.OrderMenuProduct.AddRangeAsync(productsOrderRelation);
            await _context.SaveChangesAsync();
            return products;
        }

        public async Task<bool> IsTerminalStatus(Guid id)
        {
            return await _context.Orders.Where(x => x.Id == id).Select(x => x.IsTerminalStatus).FirstOrDefaultAsync();
        }
    }
}
