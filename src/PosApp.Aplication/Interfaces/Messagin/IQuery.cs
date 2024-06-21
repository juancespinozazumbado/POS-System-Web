using MediatR;

namespace PosApp.Aplication.Interfaces.Messagin;

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}