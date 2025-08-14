using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EMS.Application.PagedResult<Employee>> GetPagedAsync(string? search, int page, int pageSize, string sortBy, string sortDir, int? departmentId);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee e);
        Task UpdateAsync(Employee e);
        Task DeleteAsync(int id);
    }
}
