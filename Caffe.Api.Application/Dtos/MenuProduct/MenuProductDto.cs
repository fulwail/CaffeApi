using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Dtos.MenuProduct
{
    public class MenuProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
