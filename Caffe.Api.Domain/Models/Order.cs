using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Models
{
    public class Order : Entity
    {
        public required string VisitorName { get; set; }
        public DateTime Created { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatusType Status { get; set; }
        [NotMapped]
        public bool IsTerminalStatus => Status == OrderStatusType.Completed || Status == OrderStatusType.Canceled;
        public List<OrderMenuProduct> Products { get; set; }
    }
}
