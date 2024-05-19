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
                Id= new Guid("fc1d602c-685f-440e-9fc9-bbd4babed4fb"),
                Name="Iphone Xs",
                Description="This products i produced  then then then." ,
                ImageFile="product1.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}
                
            },
             new Product()
            {
                Id= new Guid("a414c196-1d29-4482-ab55-395327586722"),
                Name="Iphone 6s",
                Description="This products i produced in the ABS then then then." ,
                ImageFile="product2.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}

            },
              new Product()
            {
                Id= new Guid("289981f9-60ca-4ec7-b964-e9b684efbf1f"),
                Name="Iphone 12",
                Description="This products i produced  in japonoise  then then then." ,
                ImageFile="product3.png",
                Price=900.00M,
                Category=new List<string>{"Smartphone"}

            }, new Product()
            {
                Id= new Guid("919376e2-a0b4-4986-9e24-8e572bb5174f"),
                Name="Iphone Xs",
                Description="This products i produced in Canada  then then then." ,
                ImageFile="product4.png",
                Price=900.00M,
                Category=new List<string>{"Phone"}

            }
        };
    }
}
