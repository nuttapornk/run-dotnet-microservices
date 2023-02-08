using Catalog.Api.Data.Interface;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;
    public ProductRepository(ICatalogContext context)
    {
        _context= context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context.Propducts.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
        return await _context.Propducts.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p=>p.Name, name);
        return await _context.Propducts.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Propducts.Find(p=>true).ToListAsync();    
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Propducts.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _context.Propducts.ReplaceOneAsync(filter: g=>g.Id == product.Id,replacement: product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await _context.Propducts.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}
