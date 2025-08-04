namespace Domain.IRepositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserRelationshipsRepository UserRelationshipsRepository { get; }
    UserManager<ApplicationUser> UserManager { get; }
    Task SaveChangesAsync();
}