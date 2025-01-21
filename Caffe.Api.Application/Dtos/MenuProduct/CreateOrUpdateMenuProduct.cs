using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Dtos.MenuProduct
{
    public class CreateOrUpdateMenuProduct
    {
        [Required]
        public required string Name { get; set; }
    }
}
