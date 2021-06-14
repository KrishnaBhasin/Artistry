﻿using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context , ILoggerFactory loggerFactory )
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach(var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/SeedData/types.json");

                    var type = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in type)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex.Message);
            }

        }
    }
}
