using PosApp.Dommain.Common;

namespace Doamin.Products;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? PictureURI { get; set; }

    public string? Sku {  get; set; }
    public Guid CategoryId { get; set; }
    public ProductCategory? Category { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } 


 }