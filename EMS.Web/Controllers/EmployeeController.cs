using EMS.Application.Interfaces;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _logger = logger;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10, string sortBy = "LastName", string sortDir = "asc", int? departmentId = null)
        {
            var employees = await _employeeService.GetPagedAsync(search, page, pageSize, sortBy, sortDir, departmentId);
            var departments = await _departmentService.GetAllAsync();

            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDir = sortDir;
            ViewBag.DepartmentId = departmentId;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,HireDate,Salary,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.CreateAsync(employee);
                    TempData["SuccessMessage"] = "Employee created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating employee");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload departments for the dropdown
            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,HireDate,Salary,DepartmentId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateAsync(employee);
                    TempData["SuccessMessage"] = "Employee updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload departments for the dropdown
            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Employee deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
