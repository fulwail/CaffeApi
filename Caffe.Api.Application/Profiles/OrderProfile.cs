using AutoMapper;
using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Application.Dtos.Order;
using Caffe.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                   .ForMember(d => d.Products, opt => opt.MapFrom(src => src.Products.Select(x=>x.ProductMenu)));
        }
    }
}
