using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class MenuProduct: SafeDeletableEntity
    {
        public required string Name { get; set; }
    }
}
