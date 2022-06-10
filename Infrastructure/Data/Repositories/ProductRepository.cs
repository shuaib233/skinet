using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _db;

        public ProductRepository(StoreContext context)
        {
         _db = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
          return await _db.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
           return await _db.Products
           .Include(x=>x.ProductBrand)
           .Include(x=>x.ProductType)
           .FirstOrDefaultAsync(x=>x.Id==Id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
          return await _db.Products
           .Include(x=>x.ProductBrand)
           .Include(x=>x.ProductType)
           .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
          return await _db.ProductTypes.ToListAsync();
        }
    }
}