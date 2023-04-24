using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetCatalog();
    Task<CatalogModel> GetCatalog(string id);
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> CreateCatalog(CatalogModel catalog);
}
