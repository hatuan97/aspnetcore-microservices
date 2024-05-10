using Infrastructure.Extensions;
using System.Linq.Dynamic.Core;
using Product.API.Entities;

namespace Product.API.Extensions;

public static class RepositoryProductExtensions
{
    public static IQueryable<CatalogProduct> FilterProducts(this IQueryable<CatalogProduct> queryable, decimal minPrice, decimal maxPrice) =>
        queryable.Where(e => (e.Price >= minPrice && e.Price <= maxPrice));
    
    public static IQueryable<CatalogProduct> SearchTerm(this IQueryable<CatalogProduct> queryable, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return queryable;
        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return queryable.Where(e => e.Name.ToLower()
            .Contains(lowerCaseTerm));
    }

    public static IQueryable<CatalogProduct> SortBy(this IQueryable<CatalogProduct> queryable, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return queryable.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<CatalogProduct>(orderByQueryString);
        if (string.IsNullOrWhiteSpace(orderQuery))
            return queryable.OrderBy(e => e.Name);

        return queryable.OrderBy(orderQuery);
    }
}