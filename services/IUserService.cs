using academ_sync_back.Models;

namespace academ_sync_back.services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
