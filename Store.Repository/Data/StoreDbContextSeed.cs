using Store.Core.Entities;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Store.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context)
        {

            //brand seeding:
            if (_context.Brands.Count() == 0)
            {

                //read
                var brandsData = File.ReadAllText(@"..\\Store.Repository\\Data\\DataSeed\\brands.json");
                //convert
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                //seed data to db
                if (brands != null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }

            //Type seeding:
            if (_context.Types.Count() == 0)
            {
                //read
                var typesData = File.ReadAllText(@"..\\Store.Repository\\Data\\DataSeed\\types.json");
                //convert
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                //seed data to db
                if (types != null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }
               
            //Product seeding:
            if (_context.Products.Count() == 0)
            {
                //read
                var productsData = File.ReadAllText(@"..\\Store.Repository\\Data\\DataSeed\\products.json");
                //convert
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                //seed data to db
                if (products != null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
