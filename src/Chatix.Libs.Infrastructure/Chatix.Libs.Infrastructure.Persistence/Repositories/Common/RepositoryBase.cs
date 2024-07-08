using System.Linq.Expressions;
using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories.Common;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryChatixDbContext repositoryAppDbContext;

    protected RepositoryBase(RepositoryChatixDbContext repositoryChatixDbContext)
    {
        repositoryAppDbContext = repositoryChatixDbContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges ? repositoryAppDbContext.Set<T>().AsNoTracking() : repositoryAppDbContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges ? repositoryAppDbContext.Set<T>().Where(expression).AsNoTracking() : repositoryAppDbContext.Set<T>().Where(expression);
    }

    public async virtual Task CreateAsync(T entity)
    {
        await repositoryAppDbContext.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        repositoryAppDbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        repositoryAppDbContext.Set<T>().Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await repositoryAppDbContext.SaveChangesAsync();
    }

    public void Attach(T entity)
    {
        repositoryAppDbContext.Set<T>().Attach(entity);
    }

    public void Detach(T entity)
    {
        var entry = repositoryAppDbContext.Entry(entity);
        if (entry != null)
        {
            entry.State = EntityState.Detached;
        }
    }

}