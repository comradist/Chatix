using System.Linq.Expressions;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories.Common;

public abstract class GenericRepositoryManager<T, K> : RepositoryBase<T>, IGenericRepositoryManager<T, K> where T : class
{
    protected GenericRepositoryManager(RepositoryChatixDbContext repositoryChatixDbContext) : base(repositoryChatixDbContext)
    {
    }

    public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return await FindByCondition(expression, trackChanges).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync(bool trackChanges)
    {
        var employees = await FindAll(false)
            .ToListAsync();

        return employees;
    }

    public async override Task CreateAsync(T entity)
    {
        await base.CreateAsync(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Update(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Delete(entity);
        await SaveChangesAsync();
    }
}