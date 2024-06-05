using academ_sync_back.Models;
using Microsoft.EntityFrameworkCore;

namespace academ_sync_back.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyDbContext _dbContext;

        public StudentRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.Include(s => s.User).ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await GetByIdAsync(id);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}
