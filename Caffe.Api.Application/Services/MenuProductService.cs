using AutoMapper;
using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Services
{
    public class MenuProductService : IMenuProductService
    {
        private readonly IMenuProductRepository _repository;
        private readonly IMapper _mapper;

        public MenuProductService(IMenuProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateMenuProduct(CreateOrUpdateMenuProduct createItem)
        {
            return await _repository.CreateMenuProduct(createItem.Name);
        }

        public async Task DeleteMenuProduct(Guid id)
        {
           await _repository.DeleteMenuProduct(id);
        }

        public async Task<IReadOnlyCollection<MenuProductDto>> GetMenus()
        {
           var entities=await _repository.GetMenus();
           return _mapper.Map<IReadOnlyCollection<MenuProductDto>> (entities);
        }

        public async Task<bool> IsDuplicateName(string name, Guid? id = null)
        {
           return await _repository.IsDuplicateName(name, id);
        }

        public async Task RestoreMenuProduct(Guid id)
        {
            await _repository.RestoreMenuProduct(id);
        }
    }
}
