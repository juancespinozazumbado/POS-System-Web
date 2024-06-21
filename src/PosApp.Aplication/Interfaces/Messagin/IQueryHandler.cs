using MediatR;

namespace PosApp.Aplication.Interfaces.Messagin;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
   where TQuery : IQuery<TResponse> 
{

}