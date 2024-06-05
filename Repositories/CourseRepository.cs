using academ_sync_back.Models;
using Microsoft.EntityFrameworkCore;

namespace academ_sync_back.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MyDbContext _dbContext;

        public CourseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _dbContext.Courses.FindAsync(id);
        }
        public async Task<List<Course>> GetCoursesByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Courses.Where(c => c.TeacherId == teacherId).ToListAsync();
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
        }
    }
}
