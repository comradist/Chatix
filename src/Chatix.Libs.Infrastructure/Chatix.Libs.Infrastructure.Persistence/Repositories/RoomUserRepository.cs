using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories;

public class RoomUserRepository : GenericRepositoryManager<RoomUser, Guid>, IRoomUserRepository
{
    public RoomUserRepository(RepositoryChatixDbContext repositoryChatixDbContext) : base(repositoryChatixDbContext)
    {

    }


}