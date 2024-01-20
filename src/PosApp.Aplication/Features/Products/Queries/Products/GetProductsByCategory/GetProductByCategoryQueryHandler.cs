using Doamin.Products;
using PosApp.Aplication.Common;
using PosApp.Aplication.Interfaces;
using PosApp.Dommain.Extensions;

namespace PosApp.Aplication.Features.Queries.Products.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler
   : IQueryHandler<GetProductsByCategoryQuery, Result<PaginateList<ProductsByCategoryDto>>>
{
    public Task<Result<PaginateList<ProductsByCategoryDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
