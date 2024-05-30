using academ_sync_back.Models;
using academ_sync_back.Repositories;
using academ_sync_back.requests;

namespace academ_sync_back.services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        private readonly ITeacherRepository _teacherRepository;

        public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
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
