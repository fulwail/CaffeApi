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

        public MenuProductController(IMenuProductService service)
        {
            _service = service;
        }
 
        [HttpGet()]
        public async Task<ActionResult<IReadOnlyCollection<MenuProductDto>>> GetMenus()
        {
            var items =await _service.GetMenus();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMenuProduct([FromBody] CreateOrUpdateMenuProduct createItem)
        {
            bool isDuplicate= await _service.IsDuplicateName(createItem.Name);
            if (isDuplicate)
            {
                return BadRequest("ƒанный продукт уже существует в меню");
            }
            var id = await _service.CreateMenuProduct(createItem);
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateMenuProduct([FromBody] CreateOrUpdateMenuProduct createItem, [FromRoute] Guid id)
        {
            bool isDuplicate = await _service.IsDuplicateName(createItem.Name);
            if (isDuplicate)
            {
                return BadRequest("ƒанный продукт уже существует в меню");
            }
            await _service.CreateMenuProduct(createItem);
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
