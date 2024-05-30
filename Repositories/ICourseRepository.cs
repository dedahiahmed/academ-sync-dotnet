using academ_sync_back.Models;

namespace academ_sync_back.Repositories
{
    public interface ICourseRepository
    {

        Task<Course> GetByIdAsync(int id);
        Task<List<Course>> GetAllAsync();
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
    }
}
