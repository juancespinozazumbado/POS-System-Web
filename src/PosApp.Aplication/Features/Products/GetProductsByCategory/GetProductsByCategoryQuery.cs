using Doamin.Products;
using PosApp.Aplication.Common;
using PosApp.Aplication.Interfaces.Messagin;
using PosApp.Dommain.Extensions;

namespace PosApp.Aplication.Features.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery
   (Guid categoryId, int? pageZise) : IQuery<Result<PaginateList<ProductsByCategoryDto>>>;
