using EMS.Application;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EMS.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EMS.Application.PagedResult<Employee>> GetPagedAsync(string? search, int page, int pageSize, string sortBy, string sortDir, int? departmentId)
        {
            try
            {
                var query = _context.Employees
                    .Include(e => e.Department)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(e => 
                        e.FirstName.Contains(search) || 
                        e.LastName.Contains(search) || 
                        e.Email.Contains(search));
                }

                // Apply department filter
                if (departmentId.HasValue)
                {
                    query = query.Where(e => e.DepartmentId == departmentId.Value);
                }

                // Apply business rules
                query = query.Where(e => e.Salary > 0 && e.HireDate <= DateTime.Today);

                // Get total count before paging
                var totalCount = await query.CountAsync();

                // Apply sorting with special handling for decimal fields
                query = ApplySorting(query, sortBy, sortDir);

                // Apply paging
                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new EMS.Application.PagedResult<Employee>(totalCount, items, page, pageSize);
            }
            catch (Exception ex)
            {
                // Log the error and fall back to client-side sorting if needed
                // For now, we'll rethrow but in production you might want to log this
                throw new InvalidOperationException($"Error retrieving employees: {ex.Message}", ex);
            }
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, string? sortBy, string? sortDir)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            }

            var sortDirection = sortDir?.ToLower() == "desc" ? "descending" : "ascending";
            
            // Handle different field types to avoid SQLite issues
            switch (sortBy.ToLower())
            {
                case "salary":
                    // Handle decimal field by casting to double for SQLite compatibility
                    if (sortDirection == "ascending")
                    {
                        return query.OrderBy(e => (double)e.Salary);
                    }
                    else
                    {
                        return query.OrderByDescending(e => (double)e.Salary);
                    }
                
                case "hiredate":
                    // Handle date field
                    if (sortDirection == "ascending")
                    {
                        return query.OrderBy(e => e.HireDate);
                    }
                    else
                    {
                        return query.OrderByDescending(e => e.HireDate);
                    }
                
                case "firstname":
                case "lastname":
                case "email":
                    // Handle string fields with dynamic sorting
                    try
                    {
                        return query.OrderBy($"{sortBy} {sortDirection}");
                    }
                    catch
                    {
                        // Fallback to explicit sorting if dynamic sorting fails
                        return ApplyExplicitStringSorting(query, sortBy, sortDirection);
                    }
                
                default:
                    // Default sorting
                    return query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            }
        }

        private IQueryable<Employee> ApplyExplicitStringSorting(IQueryable<Employee> query, string sortBy, string sortDirection)
        {
            switch (sortBy.ToLower())
            {
                case "firstname":
                    return sortDirection == "ascending" 
                        ? query.OrderBy(e => e.FirstName)
                        : query.OrderByDescending(e => e.FirstName);
                
                case "lastname":
                    return sortDirection == "ascending" 
                        ? query.OrderBy(e => e.LastName)
                        : query.OrderByDescending(e => e.LastName);
                
                case "email":
                    return sortDirection == "ascending" 
                        ? query.OrderBy(e => e.Email)
                        : query.OrderByDescending(e => e.Email);
                
                default:
                    return query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            // Validate business rules
            if (employee.Salary <= 0)
                throw new InvalidOperationException("Salary must be greater than 0.");

            if (employee.HireDate > DateTime.Today)
                throw new InvalidOperationException("Hire date cannot be in the future.");

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            // Validate business rules
            if (employee.Salary <= 0)
                throw new InvalidOperationException("Salary must be greater than 0.");

            if (employee.HireDate > DateTime.Today)
                throw new InvalidOperationException("Hire date cannot be in the future.");

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
