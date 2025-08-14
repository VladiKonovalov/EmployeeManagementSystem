using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department d);
        Task UpdateAsync(Department d);
        Task DeleteAsync(int id); // throw exception if employees exist
    }
}
