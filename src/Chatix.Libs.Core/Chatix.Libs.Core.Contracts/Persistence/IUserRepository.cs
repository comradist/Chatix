using Chatix.Libs.Core.Models.Entities.Chatix;

namespace Chatix.Libs.Core.Contracts.Persistence;

public interface IUserRepository : IGenericRepositoryManager<User, Guid>
{
    Task<User> GetUserByIdAsync(Guid id, bool trackChanges);
}