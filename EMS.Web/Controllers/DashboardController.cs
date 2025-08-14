using EMS.Application.Interfaces;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public DashboardController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var dashboardViewModel = new DashboardViewModel();

            // Get all employees for statistics
            var allEmployees = await _employeeService.GetPagedAsync(null, 1, int.MaxValue, "LastName", "asc", null);
            var employees = allEmployees.Items.ToList();

            // Calculate total employees
            dashboardViewModel.TotalEmployees = employees.Count;

            // Calculate average salary
            if (employees.Any())
            {
                dashboardViewModel.AverageSalary = employees.Average(e => e.Salary);
            }

            // Get departments with employee counts
            var departments = await _departmentService.GetAllAsync();
            dashboardViewModel.DepartmentStats = departments.Select(d => new DepartmentStat
            {
                DepartmentName = d.Name,
                EmployeeCount = d.Employees.Count
            }).OrderByDescending(d => d.EmployeeCount).ToList();

            // Get recent hires (last 30 days)
            var thirtyDaysAgo = DateTime.Today.AddDays(-30);
            dashboardViewModel.RecentHires = employees
                .Where(e => e.HireDate >= thirtyDaysAgo)
                .OrderByDescending(e => e.HireDate)
                .Take(10)
                .ToList();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                dashboardViewModel.SearchResults = employees
                    .Where(e => e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                               e.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                               e.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .Take(10)
                    .ToList();
                dashboardViewModel.SearchTerm = search;
            }

            return View(dashboardViewModel);
        }
    }

    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public decimal AverageSalary { get; set; }
        public List<DepartmentStat> DepartmentStats { get; set; } = new();
        public List<Employee> RecentHires { get; set; } = new();
        public List<Employee> SearchResults { get; set; } = new();
        public string? SearchTerm { get; set; }
    }

    public class DepartmentStat
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
}
