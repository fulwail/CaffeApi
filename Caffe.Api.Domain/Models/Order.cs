using Caffe.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models
{
    public class Order : Entity
    {
        public required string VisitorName { get; set; }
        public DateTime Created { get; set; }
        public PaymentType PaymentType {get;set;}
        public OrderStatusType Status { get; set; }
        public List<OrderMenuProduct> Products { get; set; }
    }
}
