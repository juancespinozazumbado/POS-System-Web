using MediatR;

namespace PosApp.Dommain.Common.Events;

public interface IDomainEventHandler<in TDomianEvent>: INotificationHandler<TDomianEvent> 
  where TDomianEvent : IDoaminEvent 
{

}