using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models.Core
{
    public abstract class SafeDeletableEntity : Entity
    {
        public bool IsDeleted { get; set; }
        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
