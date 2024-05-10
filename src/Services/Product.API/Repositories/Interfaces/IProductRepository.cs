using Contracts.Domains.Interfaces;
using Infrastructure.Common.Models;
using Product.API.Entities;
using Product.API.Extensions;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<CatalogProduct, long, ProductContext>
{
    Task<PagedList<CatalogProduct>> GetProductsAsync(ProductParameters parameters, bool trackChanges);
    Task<IEnumerable<CatalogProduct>> GetProductsAsync();
    Task<CatalogProduct?> GetProductAsync(long id);
    Task<CatalogProduct?> GetProductByNoAsync(string productNo);
    Task CreateProductAsync(CatalogProduct product);
    Task UpdateProductAsync(CatalogProduct product);
    Task DeleteProductAsync(long id);
}