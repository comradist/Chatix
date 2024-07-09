using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories;

public class MessageRepository : GenericRepositoryManager<Message, Guid>, IMessageRepository
{
    public MessageRepository(RepositoryChatixDbContext repositoryChatixDbContext) : base(repositoryChatixDbContext)
    {

    }

    public async Task<Message> GetMessageByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();
    }


}