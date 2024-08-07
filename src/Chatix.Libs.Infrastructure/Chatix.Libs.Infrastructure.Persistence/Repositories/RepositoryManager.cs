using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Infrastructure.Persistence;

namespace Chatix.Libs.Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryChatixDbContext repositoryContext;

    private readonly Lazy<IRoomRepository> roomRepository;

    private readonly Lazy<IMessageRepository> messageRepository;

    private readonly Lazy<IUserRepository> userRepository;

    private readonly Lazy<IRoomUserRepository> roomUserRepository;

    public RepositoryManager(RepositoryChatixDbContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
        roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
        messageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(repositoryContext));
        userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        roomUserRepository = new Lazy<IRoomUserRepository>(() => new RoomUserRepository(repositoryContext));
    }
    
    public IRoomRepository Room => roomRepository.Value;

    public IMessageRepository Message => messageRepository.Value;

    public IUserRepository User => userRepository.Value;

    public IRoomUserRepository RoomUser => roomUserRepository.Value;

    public async Task SaveChangesAsync()
    {
        await repositoryContext.SaveChangesAsync();
    }
}