using Caffe.Api.Domain;
using Caffe.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Infrastructure
{
    public class CaffeContext : DbContext, ICaffeContext
    {
        public DbSet<MenuProduct> MenuProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuProduct> OrderMenuProduct { get; set; }
        public CaffeContext(DbContextOptions<CaffeContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuProduct>()
                .HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
