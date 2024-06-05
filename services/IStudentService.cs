using academ_sync_back.Models;
using academ_sync_back.requests;

namespace academ_sync_back.services
{
    public interface IStudentService
    {
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Student>> GetAllStudentsAsync();
        Task AddStudentAsync(StudentRequest request);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
