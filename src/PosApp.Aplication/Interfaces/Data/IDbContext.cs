
using Microsoft.EntityFrameworkCore;

namespace PosApp.Aplication.Interfaces.Data;

public interface IDbContext
{
    /// <summary>
    /// Db set for execute complex Queries directly for the ef dbcontext
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}
