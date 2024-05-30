using academ_sync_back.Models;
using academ_sync_back.requests;

namespace academ_sync_back.services
{
    public interface ITeacherService
    {
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<List<Teacher>> GetAllTeachersAsync();
        Task AddTeacherAsync(TeacherRequest request);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
