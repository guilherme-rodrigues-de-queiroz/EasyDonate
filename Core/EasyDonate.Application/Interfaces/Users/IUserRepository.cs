using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Users;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
}