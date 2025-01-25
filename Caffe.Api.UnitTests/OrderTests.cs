using Caffe.Api.Application.Services;
using Caffe.Api.Controllers;
using Caffe.Api.Domain;
using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;
using Caffe.Api.Infrastructure;
using Caffe.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Caffe.Api.UnitTests
{
    public class OrderTests
    {
        [Fact]
        public async Task OrderWithTerminalStatusCannotChangeStatus ()
        {
            var orderGuid = Guid.NewGuid();
            var serviceMock = new Mock<IOrderService>();
            serviceMock.Setup(service => service.IsTerminalStatus(orderGuid))
                .Returns(Task.FromResult(true));
            var orderController = new OrderController(serviceMock.Object);

            var result = await orderController.ChangeStatus(orderGuid, OrderStatusType.Canceled);
         
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task OrderChangeMenuProductCount()
        {
            var product1 = new MenuProduct
            {
                Id = Guid.NewGuid(),
                Name = "Продукт 1"
            };
            var product2= new MenuProduct
            {
                Id = Guid.NewGuid(),
                Name = "Продукт 2"
            };
            var product3 = new MenuProduct
            {
                Id = Guid.NewGuid(),
                Name = "Продукт 3"
            };
            var products = new List<MenuProduct>() { product1, product2, product3 };

            var orderId = Guid.NewGuid();
            var orderMenuProduct = new OrderMenuProduct()
            {
                Id = Guid.NewGuid(),
                ProductMenuId = product1.Id,
                ProductMenu = product1,
                OrderId = orderId

            };
            var order = new Order()
            {
                Id = orderId,
                VisitorName="Тестовый посетитель",
                Products = new List<OrderMenuProduct>()
                {
                    orderMenuProduct
                }

            };

            var contextMock = new Mock<ICaffeContext>();
        
            contextMock.Setup(x => x.MenuProducts).ReturnsDbSet(products);
            contextMock.Setup(x => x.OrderMenuProduct)
                .ReturnsDbSet(new List<OrderMenuProduct> { orderMenuProduct });

            contextMock.Setup(x => x.Orders)
                .ReturnsDbSet(new List<Order> { order });

            var repository = new OrderRepository(contextMock.Object);


            var productMenuIds1 = new Guid[] { product1.Id,product2.Id, product3.Id };
            var result1 = await repository.ChangeProductMenuList(order.Id, productMenuIds1);
          
            Assert.Equal(productMenuIds1.Count(), result1.Count());
           
            var productMenuIds2 = new Guid[] { product3.Id };
            var result2 = await repository.ChangeProductMenuList(order.Id, productMenuIds2);
            Assert.Equal(productMenuIds2.Count(), result2.Count());
           
            var result3 = result2.FirstOrDefault();
            Assert.Equal(product3.Name, result3.Name);
        }
    }
}
