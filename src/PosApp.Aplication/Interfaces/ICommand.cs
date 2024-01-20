using MediatR;

namespace PosApp.Aplication.Interfaces;

public interface ICommand<out TResponse> : IRequest<TResponse>
{


}