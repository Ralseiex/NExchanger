using Microsoft.EntityFrameworkCore;
using NExchanger.Persistence;
using NExchanger.Persistence.Entities;

namespace NExchanger.Services.Users;

public class UserService : IUserService
{
    private readonly NExchangerContext _dbContext;

    public UserService(NExchangerContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateUser(string userName, string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(user => user.Name == userName, cancellationToken);
        if (user is not null) throw new UsernameTakenException();

        var newUser = new User()
        {
            Name = userName,
            Email = email,
            Created = DateTime.UtcNow
        };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .ToListAsync(cancellationToken);

        return users.Select(user => new UserDto(user.Name, user.Email));
    }

    public async Task<UserDto> GetUser(string userName, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(user => user.Name == userName, cancellationToken);
        if (user is null) throw new UserNotFoundException();

        return new UserDto(user.Name, user.Email);
    }
}