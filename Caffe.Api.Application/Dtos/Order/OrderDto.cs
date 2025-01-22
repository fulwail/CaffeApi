using Caffe.Api.Application.Dtos.MenuProduct;
using Caffe.Api.Domain.Enums;
using Caffe.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Application.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public required string VisitorName { get; set; }
        public DateTime Created { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatusType Status { get; set; }
        public bool IsTerminalStatus { get; set; }
        public List<MenuProductDto> Products { get; set; }
    }
}
