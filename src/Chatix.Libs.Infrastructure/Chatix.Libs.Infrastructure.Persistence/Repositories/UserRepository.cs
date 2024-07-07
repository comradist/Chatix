using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities.Chatix;
using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepositoryManager<User, Guid>, IUserRepository
{
    public UserRepository(RepositoryChatixDbContext repositoryChatixDbContext) : base(repositoryChatixDbContext)
    {

    }

    public async Task<User> GetUserByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();
    }


}