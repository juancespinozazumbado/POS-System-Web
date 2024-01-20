using Doamin.Products;

namespace PosApp.Aplication.Features.Queries.Products.GetProductsByCategory;

public record ProductsByCategoryDto(
    Guid Id, string Name, string Description,
    Category Category, int Quantity, bool HasStok, decimal Price);