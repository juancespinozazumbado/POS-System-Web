using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IRepository<T> where T : class
{
     /// <summary>
     /// Repository pathern. Domain driven desing.
     /// </summary>
     /// <param name="entity"></param>
     /// <returns></returns>
     /// /// 
    public Task<bool> Add(T entity);
    public Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? filter = null, 
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string inclideProperties = "");
    public Task<T?> GetById(string id);

    public Task<bool> Update( T entity);

    public Task<bool> Delete( T entity);

}