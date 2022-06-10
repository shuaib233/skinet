using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class SeedDataContext
    {

        public static async Task SeedAsync(StoreContext context,ILoggerFactory logger)
        {
            try{
                  
                 if(!context.ProductBrands.Any())
                 {
                     var brandsData = 
                         File.ReadAllText("../Infrastructure/Data/Seed/brands.json");
                     var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                     foreach (var brand in brands)
                     {
                         context.ProductBrands.Add(brand);
                     }
                     await context.SaveChangesAsync();
                 }

                  if(!context.ProductTypes.Any())
                 {
                     var ProductTypesData = 
                         File.ReadAllText("../Infrastructure/Data/Seed/types.json");
                     var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                     foreach (var productType in ProductTypes)
                     {
                         context.ProductTypes.Add(productType);
                     }
                     await context.SaveChangesAsync();
                 }

                  if(!context.Products.Any())
                 {
                     var productsData = 
                         File.ReadAllText("../Infrastructure/Data/Seed/products.json");
                     var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                     foreach (var product in products)
                     {
                         context.Products.Add(product);
                     }
                     await context.SaveChangesAsync();
                 }


            }catch(Exception ex)
            {

            }
        }
        
    }
}