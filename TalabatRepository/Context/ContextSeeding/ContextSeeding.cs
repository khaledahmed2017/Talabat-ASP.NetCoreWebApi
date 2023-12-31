using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Entities.OrderAggregate;

namespace TalabatRepository.Context.ContextSeeding
{
    public class ContextSeeding
    {
        public static async Task SeedAsync(StoreContext dbContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!dbContext.ProductBrands.Any())
                {
                    var BrandData = File.ReadAllText("../TalabatRepository/DataSeeding/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    foreach (var Brand in Brands)
                    {
                        dbContext.Set<ProductBrand>().Add(Brand);
                    }
                }
                if (!dbContext.ProductTypes.Any())
                {
                    var TypeData = File.ReadAllText("../TalabatRepository/DataSeeding/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                    foreach (var type in types)
                    {
                        dbContext.Set<ProductType>().Add(type);
                    }
                }
                if (!dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText("../TalabatRepository/DataSeeding/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var product in products)
                    {
                        dbContext.Set<Product>().Add(product);
                    }
                }
                if (!dbContext.DeliveryMethod.Any())
                {
                    var DeliveryData = File.ReadAllText("../TalabatRepository/DataSeeding/delivery.json");
                    var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                    foreach (var delivery in deliveries)
                    {
                        dbContext.Set<DeliveryMethod>().Add(delivery);
                    }
                }
                await dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex, ex.Message);

            }
        }
        }
}
