using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuProductController : ControllerBase
    {
       
        private readonly IMenuProductService _service;
        private const string DuplicateMessage = "ƒанный продукт уже существует в меню";
        public MenuProductController(IMenuProductService service)
        {
            _service = service;
        }
 
        [HttpGet()]
        public async Task<ActionResult<IReadOnlyCollection<MenuProductDto>>> GetMenus([FromQuery] bool showDeleted=false)
        {
            var items =await _service.GetMenus(showDeleted);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMenuProduct([FromBody] CreateOrUpdateMenuProduct createItem)
        {
            bool isDuplicate= await _service.IsDuplicateName(createItem.Name);
            if (isDuplicate)
            {
                return BadRequest(DuplicateMessage);
            }
            var id = await _service.CreateMenuProduct(createItem);
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateMenuProduct([FromBody] CreateOrUpdateMenuProduct updateItem, [FromRoute] Guid id)
        {
            bool isDuplicate = await _service.IsDuplicateName(updateItem.Name);
            if (isDuplicate)
            {
                return BadRequest(DuplicateMessage);
            }
            await _service.UpdateMenuProduct(updateItem,id);
            return Ok(id);
        }
        [HttpDelete("{id}")]
        public async Task DeleteMenuProduct(Guid id)
        {
            await _service.DeleteMenuProduct(id);
        }
        [HttpPost("restore/{id}")]
        public async Task RestoreMenuProduct(Guid id)
        {
            await _service.RestoreMenuProduct(id);
        }
    }
}
