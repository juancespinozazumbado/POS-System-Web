using MediatR;

namespace PosApp.Aplication.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}