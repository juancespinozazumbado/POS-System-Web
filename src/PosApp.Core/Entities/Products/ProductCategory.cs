
using PosApp.Dommain.Common;

namespace Doamin.Products;
public class ProductCategory : BaseEntity
{
    public string? Name { get; set; }
    public string ? Description { get; set; } 
    public IEnumerable<Product>? Products { get; set;}
}