using academ_sync_back.Models;
using Microsoft.EntityFrameworkCore;

namespace academ_sync_back.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly MyDbContext _dbContext;

        public TeacherRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _dbContext.Teachers.FindAsync(id);
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _dbContext.Teachers.ToListAsync();
        }

        public async Task AddAsync(Teacher teacher)
        {
            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _dbContext.Teachers.Update(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Teacher teacher)
        {
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
        }
    }
}
