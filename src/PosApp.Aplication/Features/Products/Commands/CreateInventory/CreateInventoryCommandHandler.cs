
using Doamin.Products;
using PosApp.Aplication.Interfaces;
using PosApp.Dommain.Extensions;
using PosApp.Dommain.Interfaces;

namespace PosApp.Aplication.Features.Products.Commands.CreateInventory;

public class CreateInventoryCommandHandler : ICommandHandler<CreateInventoryCommand, Result<int>>
{
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInventoryCommandHandler(IRepository<Inventory> repository, IUnitOfWork unitOfWork)
    {
        this._inventoryRepository = repository;
        this._unitOfWork = unitOfWork;

    }
    public async Task<Result<int>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = new Inventory();
       await  _inventoryRepository.Add(inventory);
        throw new NotImplementedException();
    }
}
