using Caffe.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Api.Domain
{
    public interface ICaffeContext
    {
        DbSet<MenuProduct> MenuProducts { get; set; }
        DbSet<OrderMenuProduct> OrderMenuProduct { get; set; }
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}