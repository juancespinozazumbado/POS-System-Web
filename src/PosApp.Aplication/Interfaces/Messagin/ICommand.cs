using MediatR;

namespace PosApp.Aplication.Interfaces.Messagin;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}