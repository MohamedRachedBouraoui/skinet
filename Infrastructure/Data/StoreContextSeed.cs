using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeeder
    {
        private const string seedingDataSrc = @"..\Infrastructure\Data\SeedData\";

        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                await SeedBrands(storeContext);
                await SeedProductTypes(storeContext);
                await SeedProducts(storeContext);
                await SeedDeliveryMethods(storeContext);
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeeder>();
                logger.LogError(ex, "An Error occured during seedings");
            }
        }

        private static async Task SeedDeliveryMethods(StoreContext storeContext)
        {
            if (storeContext.DeliveryMethods.Any() == false)
            {
                var dmData = File.ReadAllText(seedingDataSrc + "delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                storeContext.DeliveryMethods.AddRange(methods);
                await storeContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProducts(StoreContext storeContext)
        {
            if (storeContext.Products.Any() == false)
            {
                var productsData = File.ReadAllText(seedingDataSrc + "products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                storeContext.Products.AddRange(products);
                await storeContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductTypes(StoreContext storeContext)
        {
            if (storeContext.ProductTypes.Any() == false)
            {
                var productTypesData = File.ReadAllText(seedingDataSrc + "types.json");
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

                storeContext.ProductTypes.AddRange(productTypes);
                await storeContext.SaveChangesAsync();
            }
        }

        private static async Task SeedBrands(StoreContext storeContext)
        {
            if (storeContext.ProductBrands.Any() == false)
            {
                var brandsData = File.ReadAllText(seedingDataSrc + "brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                storeContext.ProductBrands.AddRange(brands);
                await storeContext.SaveChangesAsync();
            }
        }
    }
}