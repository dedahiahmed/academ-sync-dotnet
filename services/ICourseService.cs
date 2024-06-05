using academ_sync_back.Models;
using academ_sync_back.requests;
using System.Security.Claims;

namespace academ_sync_back.services
{
    public interface ICourseService
    {
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Course>> GetAllCoursesAsync(ClaimsPrincipal user);
        Task AddCourseAsync(CourseRequest course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}
