using AutoMapper;
using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Application.Dtos.Order;
using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;
using Caffe.Api.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateOrder(CreateOrder createOrderDto)
        {
            var entity = new Order()
            {
                Id = Guid.NewGuid(),
                VisitorName = createOrderDto.VisitorName,
                Created = DateTime.UtcNow,
                PaymentType = createOrderDto.PaymentType,
                Status = OrderStatusType.InProgress,
                Products = new List<OrderMenuProduct>()
            };

            var productsOrderRelation = createOrderDto.MenuProductIds.Select(x => new OrderMenuProduct()
            {
                Id = Guid.NewGuid(),
                OrderId = entity.Id,
                ProductMenuId = x,
            });
            entity.Products.AddRange(productsOrderRelation);
            return await _repository.AddOrder(entity);
        }

        public async Task DeleteOrder(Guid id)
        {
            await _repository.DeleteOrder(id);
        }

        public async Task ChangeStatus(Guid id, OrderStatusType status)
        {
            await _repository.ChangeStatus(id, status);
        }
        public async Task<IReadOnlyCollection<OrderDto>> GetOrders(DateTime dateBegin, DateTime dateEnd, OrderStatusType status)
        {
            var entities = await _repository.GetOrders(dateBegin, dateEnd, status);
            return _mapper.Map<IReadOnlyCollection<OrderDto>>(entities);
        }
        public async Task<IReadOnlyCollection<MenuProductDto>> ChangeProductMenuList(Guid orderId, Guid[] menuProductIds)
        {
            var entities = await _repository.ChangeProductMenuList(orderId, menuProductIds);
            return _mapper.Map<IReadOnlyCollection<MenuProductDto>>(entities);
        }

        public async Task<bool> IsTerminalStatus(Guid id)
        {
            return await _repository.IsTerminalStatus(id);
        }
    }
}
