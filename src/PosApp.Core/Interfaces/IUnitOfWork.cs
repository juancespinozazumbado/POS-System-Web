using PosApp.Dommain.Extensions;

namespace PosApp.Dommain.Interfaces;


public interface IUnitOfWork
{
    public Task<Result> SaveChangesAsync();
}