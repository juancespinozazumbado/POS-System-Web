using PosApp.Dommain.Interfaces;
using System.Linq.Expressions;

namespace PosApp.Infraestructure.Persitence.Repository;

public class BaseRepository<T> : IRepository<T> where T : class
{
    public Task<bool> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string inclideProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }
}
