using MediatR;

namespace PosApp.Dommain.Common.Events;

public interface IDoaminEvent : INotification
{
    public  DateTime DateCreated {get; protected set;}

}