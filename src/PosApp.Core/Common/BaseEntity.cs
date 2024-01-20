namespace PosApp.Dommain.Common;

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
        this.DateCreated = DateTime.Today.Date;

    }
    public Guid Id {get; protected set;}
    public DateTime DateCreated {get; protected set;}
    public DateTime DeateUpdated{get; protected set;}
    public Guid UserId {get; protected set;}
}