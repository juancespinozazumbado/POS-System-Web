using PosApp.Domain.Interface;
using System.Linq.Expressions;

namespace PosApp.Dommain.Interface;

public interface IRepository<T> where T : class
{
     /// <summary>
     /// Repository pathern. Domain driven desing.
     /// </summary>
     /// <param name="entity"></param>
     /// <returns></returns>
     /// /// 
    public Task<bool> AddAsync(T entity);

    public Task<bool> AddRangeAsync(IEnumerable<T> entities);  
    
    public Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

    //public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, 
    //Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string inclideProperties = "");

    public Task<T> Get(ISpecification<T> specification, CancellationToken cancellation);

    public IQueryable<T> GetAsQuerable();
    public Task<T?> GetByIdAsync(Guid id);

    public Task<bool> UpdateAsync( T entity);

    public Task<bool> DeleteAsync( T entity);

}