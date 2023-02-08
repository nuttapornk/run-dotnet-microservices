using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data.Interface;

public interface ICatalogContext
{
    IMongoCollection<Product> Propducts { get; }
}
