using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models
{
    public class OrderMenuProduct: Entity
    {
        public Guid OrderId {  get; set; }  
        public Order Order { get; set; }
        public Guid ProductMenuId { get; set; }
        public MenuProduct ProductMenu { get; set; }
    }
}
