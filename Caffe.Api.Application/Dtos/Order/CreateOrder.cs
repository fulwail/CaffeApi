using Caffe.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Dtos.Order
{
    public class CreateOrder
    {
        [Required]
        public required string VisitorName { get; set; }
        [Required]
        public PaymentType PaymentType { get;  set; }
        public Guid[] MenuProductIds { get; set; }
    }
}
