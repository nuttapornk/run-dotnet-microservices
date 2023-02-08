using Catalog.Api.Data.Interface;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

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
