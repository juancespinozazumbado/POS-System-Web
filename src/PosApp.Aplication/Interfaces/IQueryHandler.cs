using MediatR;

namespace PosApp.Aplication.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
   where TQuery : IQuery<TResponse> 
   {
      
   }