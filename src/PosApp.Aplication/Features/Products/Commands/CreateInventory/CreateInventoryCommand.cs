
using Doamin.Products;
using PosApp.Aplication.Interfaces;
using PosApp.Dommain.Extensions;

namespace PosApp.Aplication.Features.Products.Commands.CreateInventory;

public sealed record  CreateInventoryCommand(string Name, int Quantity, 
    Category Category, string? Description ) : ICommand<Result<int>> {
    
    }

