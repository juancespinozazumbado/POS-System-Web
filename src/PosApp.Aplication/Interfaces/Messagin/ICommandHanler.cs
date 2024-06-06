using MediatR;
using PosApp.Aplication.Features.Products.CreateProduct;

namespace PosApp.Aplication.Interfaces.Messagin;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{
    //public Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}