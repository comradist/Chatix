
namespace Chatix.Libs.Core.Contracts.Persistence;

public interface IRepositoryManager
{

    IRoomRepository Room { get; }

    IUserRepository User { get; }

    IMessageRepository Message { get; }

    Task SaveChangesAsync();
}