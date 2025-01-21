using Caffe.Api.Domain.Interceptors;
using Caffe.Api.Domain.Models;
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
        public DbSet<MenuProduct> MenuProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuProduct> OrderMenuProduct  { get; set;}
        public CaffeContext(DbContextOptions<CaffeContext> options) : base(options)
        {
        }
    }
}
