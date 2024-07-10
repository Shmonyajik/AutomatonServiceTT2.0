using Warehouse.Domain.Models;

namespace Warehouse.DAL;

public class DbInitializer
{
    public static void Initialize(WarehouseDbContext dbContext)
    {
        if (dbContext.Storages.Any()) return;

        var storages = new List<Storage>
        {
            new Storage(),
            new Storage(),
            new Storage(),
        };

        var products = new List<Product>
        {
            new Product("Cucumber"),
            new Product("Tomato"),
            new Product("Snickers"),
            new Product("Чебупели"),
            new Product("Baikal"),
            new Product("Три корочки"),
            new Product("Добрый Кола"),
            new Product("Консервированный горошек"),
            new Product("Холодец"),
            new Product("Чечил"),
            new Product("Свиной язык"),
        };
        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();

        var random = new Random();
        foreach (var storage in storages)
        {
            foreach (var product in products.OrderBy(p => random.Next()).Take(5))
            {
                storage.AddProduct(product.Id);
            }
        }

        dbContext.Storages.AddRange(storages);

        dbContext.SaveChanges();
    }
}