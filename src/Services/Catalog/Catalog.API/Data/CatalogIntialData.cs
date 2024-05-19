using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogIntialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;
            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync();
        }
        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id= new Guid(""),
                Name="Iphone Xs",
                Description="This products i produced  then then then." ,
                ImageFile="product1.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}
                
            },
             new Product()
            {
                Id= new Guid(""),
                Name="Iphone 6s",
                Description="This products i produced in the ABS then then then." ,
                ImageFile="product2.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}

            },
              new Product()
            {
                Id= new Guid(""),
                Name="Iphone 12",
                Description="This products i produced  in japonoise  then then then." ,
                ImageFile="product3.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}

            }, new Product()
            {
                Id= new Guid(""),
                Name="Iphone Xs",
                Description="This products i produced in Canada  then then then." ,
                ImageFile="product4.png",
                Price=900.00M,
                Category=new List<string>{"Phone"}

            }
        };
    }
}
