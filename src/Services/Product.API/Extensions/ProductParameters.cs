using Shared.SeedWork;

namespace Product.API.Extensions;

public class ProductParameters : RequestParameters
{
    public ProductParameters() => OrderBy = "Name";
    public string? SearchTerm { get; set; }
    
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    
    public bool ValidPriceRange => MaxPrice > MinPrice;
}