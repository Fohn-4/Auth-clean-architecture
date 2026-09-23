using Auth.Core.Users.Models;

namespace Auth.Core.Users.Abstractions
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken ct);
    }
}