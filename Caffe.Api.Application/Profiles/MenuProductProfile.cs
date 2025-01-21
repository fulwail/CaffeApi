using AutoMapper;
using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Domain.Models;

namespace Caffe.Api.Application.Profiles
{
    public class MenuProductProfile : Profile
    {
        public MenuProductProfile()
        {
            CreateMap<MenuProduct, MenuProductDto>().ReverseMap();
        }
    }
}
