
using Doamin.Products;
using PosApp.Aplication.Interfaces.Messagin;
using PosApp.Dommain.Extensions;
using PosApp.Dommain.Interface;

namespace PosApp.Aplication.Features.Products.CreateProduct;


public sealed class  CreateProductCommand : ICommand<Result<Product>>
{
    public string Name { set; get; } = string.Empty;
    public string? Description { set; get; }
    public string? ImageUri { set; get; }
    public Guid CategorId { set; get; }
    public int Quantity { set; get; }
    public decimal Price { set; get; }

}


public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result<Product>>
{
    private readonly IRepository<Product> _inventoryRepository;
    private readonly IRepository<ProductCategory> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IRepository<Product> repository, IRepository<ProductCategory> categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _inventoryRepository = repository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var category = await _categoryRepository.GetByIdAsync(request.CategorId);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description ?? "",
            CategoryId = request.CategorId,
            Category = category,
            Quantity = request.Quantity,
            Price = request.Price


        };

        await _inventoryRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return new Result<Product>(product);
       
    }

}
