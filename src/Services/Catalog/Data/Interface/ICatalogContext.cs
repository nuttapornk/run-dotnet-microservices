using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data.Interface
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Propducts { get; }
    }
}
