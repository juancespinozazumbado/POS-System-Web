using Doamin.Products;
using PosApp.Aplication.Common;
using PosApp.Aplication.Interfaces;
using PosApp.Dommain.Extensions;

namespace PosApp.Aplication.Features.Queries.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery
   (Category category, int? pageZise ) : IQuery<Result<PaginateList<ProductsByCategoryDto>>>;
