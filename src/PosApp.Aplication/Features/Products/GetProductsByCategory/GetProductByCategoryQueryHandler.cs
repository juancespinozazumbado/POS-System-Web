using Doamin.Products;
using PosApp.Aplication.Common;
using PosApp.Aplication.Interfaces.Messagin;
using PosApp.Dommain.Extensions;

namespace PosApp.Aplication.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler
   : IQueryHandler<GetProductsByCategoryQuery, Result<PaginateList<ProductsByCategoryDto>>>
{
    public Task<Result<PaginateList<ProductsByCategoryDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        // fetch products by category





        throw new NotImplementedException();
    }
}
