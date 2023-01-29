using Catalog.Data.Interface;
using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Propducts = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);
            CatalogContextSeed.SeedData(Propducts);
        }

        public IMongoCollection<Product> Propducts { get; }
    }
}
