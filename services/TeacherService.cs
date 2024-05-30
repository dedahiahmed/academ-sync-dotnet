using academ_sync_back.Models;
using academ_sync_back.Repositories;
using academ_sync_back.requests;

namespace academ_sync_back.services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        public TeacherService(ITeacherRepository teacherRepository, IUserRepository userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetByIdAsync(id);
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }

        public async Task AddTeacherAsync(TeacherRequest request)
        {
            // Check if the user with the specified userId exists
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ArgumentException("User with the specified userId not found");
            }

            // Create a new teacher entity using the request model
            var teacher = new Teacher
            {
                Matter = request.Matter,
                PhoneNumber = request.PhoneNumber,
                UserId = request.UserId
            };

            // Add the teacher entity to the repository
            await _teacherRepository.AddAsync(teacher);
        }


        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            // Add any business logic or validation here before updating the repository
            await _teacherRepository.UpdateAsync(teacher);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            // Retrieve the teacher from the repository and then delete it
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher != null)
            {
                await _teacherRepository.DeleteAsync(teacher);
            }
            else
            {
                throw new InvalidOperationException("Teacher not found");
            }
        }
    }
}
