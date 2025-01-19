using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Enums
{
    public enum OrderStatusType
    {
        [Description("В работе")]
        InProgress,
        [Description("Выполнен")]
        Completed,
        [Description("Отменен")]
        Canceled
    }
}
