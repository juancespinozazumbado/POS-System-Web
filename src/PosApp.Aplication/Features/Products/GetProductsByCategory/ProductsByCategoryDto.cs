using Doamin.Products;

namespace PosApp.Aplication.Features.Products.GetProductsByCategory;

public record ProductsByCategoryDto(
    Guid Id, string Name, string Description,
    ProductCategory Category, int Quantity, bool HasStok, decimal Price);