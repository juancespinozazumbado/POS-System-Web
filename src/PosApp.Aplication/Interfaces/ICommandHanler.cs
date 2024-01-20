using MediatR;

namespace PosApp.Aplication.Interfaces;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
 {
     //public Task<TResponse> Handle(TCommand command);
 } 