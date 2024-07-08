using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories;

public class RoomRepository : GenericRepositoryManager<Room, Guid>, IRoomRepository
{
    public RoomRepository(RepositoryChatixDbContext repositoryChatixDbContext) : base(repositoryChatixDbContext)
    {

    }

    public async Task<Room> GetRoomByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.Id.Equals(id), trackChanges)
                .Include(x => x.Admin)
                //.Include(x => x.RoomUsers)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync();
    }


}