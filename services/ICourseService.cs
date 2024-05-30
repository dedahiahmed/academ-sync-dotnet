using academ_sync_back.Models;
using academ_sync_back.requests;

namespace academ_sync_back.services
{
    public interface ICourseService
    {
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(CourseRequest course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}
