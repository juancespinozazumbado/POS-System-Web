using PosApp.Dommain.Extensions;

namespace PosApp.Dommain.Interface;


public interface IUnitOfWork
{

    public IDbContext Context {get;}
    public Task<Result> SaveChangesAsync();
}