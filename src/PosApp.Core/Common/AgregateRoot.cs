using PosApp.Dommain.Common.Events;

namespace PosApp.Dommain.Common;

/// <summary>
/// This class is for manage and handle events, in order to follow the domain driven desing.
/// </summary>
public abstract class AgregateRoot : BaseEntity
{
    private readonly List<IDoaminEvent> _eventList = new();
        
 
    public AgregateRoot(Guid Id): base()
    {
       this.EventLiist = _eventList.AsReadOnly();
    }

    public IReadOnlyCollection<IDoaminEvent> EventLiist {get; private set;} 
    
}