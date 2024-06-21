using Doamin.Products;
using Microsoft.EntityFrameworkCore;
using PosApp.Aplication.Common;
using PosApp.Aplication.Interfaces.Data;
using PosApp.Aplication.Interfaces.Messagin;
using PosApp.Dommain.Extensions;
using PosApp.Dommain.Interface;

namespace PosApp.Aplication.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery
   , Result<PaginateList<Product>>>
{
    private readonly IDbContext _dbContext; 
    private readonly IRepository<Product> _productRepository;

    public GetProductsByCategoryQueryHandler(IDbContext dbContex, IRepository<Product> productRepository)
    {
        _dbContext = dbContex;
        _productRepository = productRepository;
    }

    public async Task<Result<PaginateList<Product>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        // fetch products by category

        var products =  _productRepository.GetAsQuerable();
        
        var items = products
            .Skip((request.pageIndex - 1) * request.pageZise).Take(request.pageZise).ToList();
        var totalItems = products.Count();


        return new Result<PaginateList<Product>>(
             response: new PaginateList<Product>( items, request.pageZise, request.pageIndex, totalItems )
        );

        throw new NotImplementedException();
    }
}
