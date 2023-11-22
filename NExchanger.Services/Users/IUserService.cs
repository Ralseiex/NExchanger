namespace NExchanger.Services.Users;

public interface IUserService
{
    Task<int> CreateUser(string userName, string email, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    Task<UserDto> GetUser(string userName, CancellationToken cancellationToken);
}