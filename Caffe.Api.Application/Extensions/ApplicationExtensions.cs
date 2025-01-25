using Caffe.Api.Application.Services;
using Caffe.Api.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Extensions
{
    public static class ApplicationExtensions
    { 
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IMenuProductService,MenuProductService >();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
