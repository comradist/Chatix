using System.Linq.Expressions;

namespace Chatix.Libs.Infrastructure.Persistence.Contracts;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> FindAll(bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    Task CreateAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}