using Caffe.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe.Api.Domain.Repositories
{
    public class MenuProductRepository : IMenuProductRepository
    {
        private readonly ICaffeContext _context;

        public MenuProductRepository(ICaffeContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateMenuProduct(string name)
        {
            var entity= new MenuProduct {
                Id=Guid.NewGuid(),
                Name = name
            };
            _context.MenuProducts.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteMenuProduct(Guid id)
        {
            var entity= await _context.MenuProducts.FindAsync(id);
            if (entity != null) 
            {
                _context.MenuProducts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<MenuProduct>> GetMenus(bool showDeleted)
        {
            var entities = _context.MenuProducts.AsQueryable();

            if (showDeleted)
                entities = entities.IgnoreQueryFilters();

            return await entities.ToListAsync();
        }

        public async Task<bool> IsDuplicateName(string name, Guid? id=null)
        {
            return await _context.MenuProducts.AnyAsync(x => x.Name.ToLower() == name.ToLower()&& x.Id!=id);
        }

        public async Task RestoreMenuProduct(Guid id)
        {
            var entity = await _context.MenuProducts.IgnoreQueryFilters().FirstOrDefaultAsync(x=>x.Id==id);
            if (entity != null)
            {
                entity.Restore();
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MenuProduct> UpdateMenuProduct(string name, Guid id)
        {
            var entity = await _context.MenuProducts.FindAsync(id);
            if (entity != null)
            {
                entity.Name = name;
                await _context.SaveChangesAsync();
            }
            return entity;
        }
    }
}
