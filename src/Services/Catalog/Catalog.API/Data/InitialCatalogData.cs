using Marten.Schema;

namespace Catalog.API.Data
{
    public class InitialCatalogData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellationToken)
        {
            using var session = store.LightweightSession();

            // Nếu đã có dữ liệu thì không seed nữa
            if (await session.Query<Product>().AnyAsync(cancellationToken))
                return;

            // Seed data
            session.Store<Product>(GetPreconfiguredProducts());

            await session.SaveChangesAsync(cancellationToken);
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new()
            {
                Id = Guid.Parse("1681684d-c00a-46fb-b3a6-850b4494b9af"),
                Name = "iPhone 15 Pro Max",
                Description = "Apple flagship smartphone with A17 Pro chip",
                ImageFile = "product-1.png",
                Price = 34990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Apple"
                }
            },

            new()
            {
                Id = Guid.Parse("7cbeee71-1ac4-4fde-9e21-aa4f5940ae64"),
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Samsung premium Android smartphone",
                ImageFile = "product-2.png",
                Price = 31990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Samsung"
                }
            },

            new()
            {
                Id = Guid.Parse("ea03bea8-c1ed-498c-a052-223c3390cc48"),
                Name = "Xiaomi 14",
                Description = "High performance Xiaomi smartphone",
                ImageFile = "product-3.png",
                Price = 19990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Xiaomi"
                }
            },

            new()
            {
                Id = Guid.Parse("386b323a-56a5-45af-b713-c4eb3c08df41"),
                Name = "Xiaomi PRO",
                Description = "High performance Xiaomi smartphone",
                ImageFile = "product-3.png",
                Price = 19990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Xiaomi"
                }
            },

            new()
            {
                Id = Guid.Parse("31e486e7-48e2-4e3b-b15e-3474d1e00955"),
                Name = "Redmi 12 Pro",
                Description = "High performance Xiaomi smartphone",
                ImageFile = "product-3.png",
                Price = 19990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Xiaomi"
                }
            },

            new()
            {
                Id = Guid.Parse("af8fc247-bbea-4f9d-8ebd-0b5f0bd407d0"),
                Name = "Poco X5",
                Description = "High performance Xiaomi smartphone",
                ImageFile = "product-3.png",
                Price = 19990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Xiaomi"
                }
            },

            new()
            {
                Id = Guid.Parse("0b04baf7-8ce6-4814-81af-11a6d913f4c4"),
                Name = "Iphone 14 promax",
                Description = "High performance Xiaomi smartphone",
                ImageFile = "product-3.png",
                Price = 19990000,
                Category = new List<string>
                {
                    "Smart Phone",
                    "Xiaomi"
                }
            }
        };
    }
}
