namespace PosApp.Dommain.Common;

/// <summary>
/// Each entitie inherit from the Base entitie
/// </summary>
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
  

    /// <summary>
    /// Unique identifier for each entity
    /// </summary>
    public Guid Id {get;}
    /// <summary>
    /// Date that this entity was created
    /// </summary>
    public DateTime DateCreated {get;}
    /// <summary>
    /// The last date of modification for each entitie
    /// </summary>
    public DateTime DeateUpdated{get; set;}

    /// <summary>
    /// The Id for the user that modified the entitie
    /// </summary>
    public Guid UserId {get; set;}
}