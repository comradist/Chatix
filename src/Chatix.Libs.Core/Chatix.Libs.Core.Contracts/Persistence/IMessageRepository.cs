using Chatix.Libs.Core.Models.Entities;

namespace Chatix.Libs.Core.Contracts.Persistence;

public interface IMessageRepository : IGenericRepositoryManager<Message, Guid>
{
    Task<Message> GetMessageByIdAsync(Guid id, bool trackChanges);
}