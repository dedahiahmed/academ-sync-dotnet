using academ_sync_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace academ_sync_back.Repositories
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetByIdAsync(int id);
        Task<List<Teacher>> GetAllAsync();
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
    }
}
