using academ_sync_back.Models;
using Microsoft.EntityFrameworkCore;

namespace academ_sync_back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _dbContext;

        public UserRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
