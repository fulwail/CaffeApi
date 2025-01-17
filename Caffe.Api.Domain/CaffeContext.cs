using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain
{
    public class CaffeContext:DbContext
    {
        public CaffeContext(DbContextOptions<CaffeContext> options) : base(options)
        {
        }
    }

}
