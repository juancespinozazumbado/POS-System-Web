using Microsoft.EntityFrameworkCore;
using PosApp.Dommain.Interface;
using PosApp.Infraestructure.Persitence.ApplicationContex;
using System.Linq.Expressions;

namespace PosApp.Infraestructure.Persitence.Repository;

public class BaseRepository<T> : IRepository<T> where T : class
{

    private readonly ApplicationDbContex _dbContex;

    public BaseRepository(ApplicationDbContex contex)
    {
        _dbContex = contex;
    }



    public Task<bool> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string inclideProperties = "")
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAsQuerable()
    {
        var properties = new object();
       return _dbContex.Set<T>().AsQueryable<T>();
    }

    public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string inclideProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
