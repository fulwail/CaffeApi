using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models
{
    public class SafeDeletableEntity : Entity
    {
        public bool IsDeleted { get; set; }
    }
}
