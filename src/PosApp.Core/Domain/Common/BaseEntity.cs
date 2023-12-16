namespace Doamin.Common;

public abstract class BaseEntity 
{

    /// <summary>
    /// Each entity on the Dommain model inhert this properties.
    /// </summary> <summary>
    /// 
    /// </summary>
    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        this.DateCreated = DateTime.Today;

    }
    public Guid Id {get; protected set;}
    public DateTime DateCreated {get; protected set;}
    public DateTime DeateDeleted {get; protected set;}
    public DateTime DateDelited {get; protected set;}
}