using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Application.Dtos.Order;
using Caffe.Api.Application.Services;
using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
       
        [HttpPost]
        public async Task<Guid> CreateOrder([FromBody] CreateOrder createOrderDto)
        {
            return await _service.CreateOrder(createOrderDto);
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid id)
        {
            await _service.DeleteOrder(id);
            return Ok();
        }
      
        [HttpPost("status/{id}")]
        public async Task<ActionResult> ChangeStatus([FromRoute] Guid id,[FromQuery] OrderStatusType status)
        {
            var isTerminalStatus= await _service.IsTerminalStatus(id);
            if(isTerminalStatus)
            {
                return BadRequest("У выбранного заказа отсутствует возможность изменить статус");
            }
            await _service.ChangeStatus(id, status);
            return Ok();
        }
       
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<OrderDto>>> GetOrders([FromQuery] DateTime dateBegin,
            [FromQuery] DateTime dateEnd, [FromQuery] OrderStatusType status)
        {
            var entities = await _service.GetOrders(dateBegin, dateEnd, status);
            return Ok(entities);
        }
       
        [HttpPut("{orderId}")]
        public async Task<ActionResult<IReadOnlyCollection<MenuProductDto>>> ChangeProductMenuList([FromRoute]Guid orderId, [FromBody] Guid[] menuProductIds)
        {
            var isTerminalStatus = await _service.IsTerminalStatus(orderId);
            if (isTerminalStatus)
            {
                return BadRequest("У заказа отсутствует возможность изменить список выбранных позиций");
            }
            var entities = await _service.ChangeProductMenuList(orderId, menuProductIds);
            return Ok(entities);
        }
 
    }
}
