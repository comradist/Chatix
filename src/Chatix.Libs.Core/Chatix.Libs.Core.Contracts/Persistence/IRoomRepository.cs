using Chatix.Libs.Core.Models.Entities;

namespace Chatix.Libs.Core.Contracts.Persistence;

public interface IRoomRepository : IGenericRepositoryManager<Room, Guid>
{
    Task<Room> GetRoomByIdAsync(Guid id, bool trackChanges);
}