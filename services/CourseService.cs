using academ_sync_back.Models;
using academ_sync_back.Repositories;
using academ_sync_back.requests;
using System.Security.Claims;

namespace academ_sync_back.services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<CourseService> _logger;
        public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<List<Course>> GetAllCoursesAsync(ClaimsPrincipal user)
        {
            var claims = user.Claims.ToList();
            var userIdClaim = claims.Count >= 3 ? claims[2].Value : null;
            var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;

            // Log the userIdClaim and roleClaim values
            _logger.LogInformation("UserIdClaim: {UserIdClaim}", userIdClaim);
            _logger.LogInformation("RoleClaim: {RoleClaim}", roleClaim);

            if (roleClaim == "TEACHER" && int.TryParse(userIdClaim, out int teacherId))
            {
                // Return only the courses where TeacherId matches the id in the token
                return await _courseRepository.GetCoursesByTeacherIdAsync(teacherId);
            }

            // Return all courses for ADMIN and STUDENT
            return await _courseRepository.GetAllAsync();
        }

        public async Task AddCourseAsync(CourseRequest request)
        {
            // Check if the teacher with the specified TeacherId exists
            var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
            if (teacher == null)
            {
                throw new ArgumentException("Teacher with the specified TeacherId not found");
            }

            var course = new Course
            {
                Title = request.Title,
                Type = request.Type,
                Semester = request.Semester,
                TeacherId = request.TeacherId,
                Files = request.Files.ToList()
            };

            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                await _courseRepository.DeleteAsync(course);
            }
            else
            {
                throw new ArgumentException("Course not found");
            }

        }
    }
}
