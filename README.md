# Employee Management System (EMS)

A modern, full-featured Employee Management System built with ASP.NET Core 8.0, featuring a clean layered architecture, comprehensive CRUD operations, advanced search and filtering, and a responsive web interface.

## 🚀 Features

- **Employee Management**: Complete CRUD operations with validation
- **Department Management**: Department CRUD with employee relationship handling
- **Advanced Search & Filtering**: Search by name/email, filter by department, sort by multiple fields
- **Pagination**: Efficient data display with configurable page sizes
- **Dashboard**: Real-time statistics and overview
- **Data Validation**: Client-side and server-side validation with custom attributes
- **Error Handling**: User-friendly error pages with detailed logging
- **Responsive Design**: Modern UI built with Bootstrap and FontAwesome
- **Docker Support**: Containerized deployment ready

## 🏗️ Tech Stack & Architecture

### **Technology Stack**
- **.NET 8.0** - Latest LTS version
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core** - ORM with SQLite
- **Serilog** - Structured logging
- **Bootstrap 5** - UI framework
- **FontAwesome** - Icons
- **Docker** - Containerization

### **Architecture (Clean Architecture)**
```
EmployeeManagementSystem/
├── EMS.Domain/           # Domain Layer
│   └── Models/          # Entities and validation attributes
├── EMS.Application/     # Application Layer
│   └── Interfaces/      # Service contracts
├── EMS.Infrastructure/  # Infrastructure Layer
│   ├── Data/           # EF Core DbContext
│   ├── Services/       # Service implementations
│   └── Extensions/     # DI configuration
└── EMS.Web/            # Presentation Layer
    ├── Controllers/    # MVC Controllers
    ├── Views/          # Razor Views
    └── Middleware/     # Custom middleware
```

### **Key Design Patterns**
- **Repository Pattern** - Data access abstraction
- **Service Layer Pattern** - Business logic encapsulation
- **Dependency Injection** - Loose coupling
- **Data Annotations** - Model validation
- **Custom Validation Attributes** - Business rule validation

## 📋 Prerequisites

### **Required**
- **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Docker** (optional) - [Download here](https://www.docker.com/products/docker-desktop/)

### **Optional**
- **dotnet-ef CLI** - For database migrations
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## 🛠️ Build and Run Instructions

### **Local Development**

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EmployeeManagementSystem
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd EMS.Web
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`
   - The application will automatically create the database and apply migrations on first run

### **Docker Deployment**

1. **Build the Docker image**
   ```bash
   docker build -t ems-app .
   ```

2. **Run the container**
   ```bash
   docker run -p 8080:8080 -v ems-data:/app/App_Data -v ems-logs:/app/Logs ems-app
   ```

3. **Access the application**
   - Open your browser and navigate to `http://localhost:8080`

### **Docker Compose (Recommended)**

1. **Create docker-compose.yml**
   ```yaml
   version: '3.8'
   services:
     ems-app:
       build: .
       ports:
         - "8080:8080"
       volumes:
         - ems-data:/app/App_Data
         - ems-logs:/app/Logs
       environment:
         - ASPNETCORE_ENVIRONMENT=Production

   volumes:
     ems-data:
     ems-logs:
   ```

2. **Run with Docker Compose**
   ```bash
   docker-compose up -d
   ```

## 🗄️ Database Migrations

### **Creating Migrations**
```bash
cd EMS.Web
dotnet ef migrations add InitialCreate
```

### **Applying Migrations**
```bash
# Apply migrations to database
dotnet ef database update

# Or let the application apply migrations automatically on startup
```

### **Database Location**
- **Local Development**: `EMS.Web/App_Data/ems.db`
- **Docker**: `/app/App_Data/ems.db` (persisted via volume)

### **Database Schema**
- **Departments**: Id, Name (unique)
- **Employees**: Id, FirstName, LastName, Email, HireDate, Salary, DepartmentId
- **Relationships**: One-to-many (Department → Employees)

## 📊 Logs and Error Handling

### **Logging Configuration**
The application uses **Serilog** with the following configuration:

- **Console Logging**: Development environment
- **File Logging**: JSON format with daily rolling files
- **Location**: `Logs/log-YYYY-MM-DD.json`
- **Retention**: 7 days of logs

### **Log Levels**
- **Information**: Application startup, database migrations
- **Warning**: Non-critical issues
- **Error**: Exceptions and errors
- **Fatal**: Application termination

### **User-Friendly Error Page**
The application includes a custom error page (`/Home/Error`) that:

- **Displays helpful messages** instead of technical details
- **Provides navigation options** (Go Back, Dashboard, Home)
- **Shows Request ID** for support purposes
- **Uses modern design** with Bootstrap styling
- **Includes guidance** on what users can do

### **Global Exception Handling**
- **Custom Middleware**: Catches unhandled exceptions
- **Automatic Logging**: Logs exceptions with context
- **User Redirection**: Redirects to error page
- **Request Context**: Includes path, method, and user agent

## 🔧 Configuration

### **Connection String**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=App_Data/ems.db"
  }
}
```

### **Environment Variables**
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- `ASPNETCORE_URLS`: Server URLs (default: http://+:8080)

## 🧪 Testing the Application

### **Sample Data**
1. **Create Departments**: Navigate to Departments → Create New
2. **Add Employees**: Navigate to Employees → Create New
3. **Test Features**:
   - Search and filter employees
   - Sort by different columns
   - Test pagination
   - Verify validation messages
   - Check error handling

### **Validation Examples**
- **Required Fields**: First Name, Last Name, Email
- **Email Format**: Must be valid email address
- **Salary Range**: Must be greater than 0
- **Hire Date**: Cannot be in the future
- **Department**: Must select a valid department

## 🚀 Deployment

### **Production Considerations**
- **Environment**: Set `ASPNETCORE_ENVIRONMENT=Production`
- **HTTPS**: Configure SSL certificates
- **Database**: Consider using a production database (SQL Server, PostgreSQL)
- **Logging**: Configure external logging service
- **Monitoring**: Add health checks and monitoring

### **Health Checks**
The application includes basic health monitoring:
- **Database connectivity**
- **Application startup**
- **Migration status**

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Check the logs in `Logs/` directory
- Review the error page for guidance
- Contact the development team with the Request ID from error pages

---

**Built with ❤️ using ASP.NET Core 8.0**

## 🎯 **Correct URLs:**

- **HTTPS**: `https://localhost:7281`
- **HTTP**: `http://localhost:5234`

## 🔍 **Why the other URLs didn't work:**

- `http://localhost:8080` - This is for Docker deployment
- `https://localhost:5001` - Default .NET 8 ports, but your project uses custom ports
- `http://localhost:5000` - Default .NET 8 ports, but your project uses custom ports

## 🚀 **Try these URLs:**

1. **Primary**: `https://localhost:7281`
2. **Fallback**: `http://localhost:5234`

## 📝 **Note about the Security Warning:**

I also noticed you have a security warning about `System.Linq.Dynamic.Core`. This is a known vulnerability. You can update it by running:

```bash
dotnet add EMS.Infrastructure package System.Linq.Dynamic.Core --version 4.0.0
```

But for now, focus on accessing the application at the correct URLs above. The application should work fine with the current version.

Try `https://localhost:7281` first - it should open your Employee Management System dashboard!

## 🚨 **Issue 2: dotnet run not working**

You're trying to run from the wrong directory. You need to run from the `EMS.Web` directory since that's the startup project:

```bash
cd EMS.Web
dotnet run
```

## 📝 **Step-by-step fix:**

1. **First, update the vulnerable package:**
   ```bash
   cd EMS.Infrastructure
   dotnet add package System.Linq.Dynamic.Core --version 4.0.0
   ```

2. **Then run the application from the correct directory:**
   ```bash
   cd ..
   cd EMS.Web
   dotnet run
   ```

3. **Or run from the solution root with project specification:**
   ```bash
   dotnet run --project EMS.Web
   ```

## 📝 **Alternative: Run from solution root**

You can also run from the solution root by specifying the project:

```bash
dotnet run --project EMS.Web
```

## 🎯 **Expected URLs after running:**

- **HTTPS**: `https://localhost:7281`
- **HTTP**: `http://localhost:5234`

Try these commands and let me know if you still have issues!

## 🔧 **Fix 1: Create the correct Employee views folder**

```bash
# Create the correct folder structure
mkdir EMS.Web\Views\Employee
```

Now let me create all the Employee view files in the correct location:

**EMS.Web/Views/Employee/Index.cshtml:**
```html
@model EMS.Application.PagedResult<EMS.Domain.Models.Employee>

@{
    ViewData["Title"] = "Employees";
}

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Employees</h3>
                    <div class="card-tools">
                        <a asp-action="Create" class="btn btn-primary">
                            <i class="fas fa-plus"></i> Create New
                        </a>
                    </div>
                </div>
                <div class="card-body">
                    @if (TempData["SuccessMessage"] != null)
                    {
                        <div class="alert alert-success alert-dismissible">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            @TempData["SuccessMessage"]
                        </div>
                    }

                    @if (TempData["ErrorMessage"] != null)
                    {
                        <div class="alert alert-danger alert-dismissible">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            @TempData["ErrorMessage"]
                        </div>
                    }

                    <!-- Search and Filter Form -->
                    <form method="get" class="mb-4">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <input type="text" name="search" class="form-control" placeholder="Search by name or email..." value="@ViewBag.Search" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <select name="departmentId" class="form-control">
                                        <option value="">All Departments</option>
                                        @foreach (var item in ViewBag.Departments)
                                        {
                                            var selected = ViewBag.DepartmentId?.ToString() == item.Value;
                                            <option value="@item.Value" selected="@selected">@item.Text</option>
                                        }
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <select name="sortBy" class="form-control">
                                        <option value="LastName" selected="@(ViewBag.SortBy == "LastName")">Last Name</option>
                                        <option value="FirstName" selected="@(ViewBag.SortBy == "FirstName")">First Name</option>
                                        <option value="Email" selected="@(ViewBag.SortBy == "Email")">Email</option>
                                        <option value="HireDate" selected="@(ViewBag.SortBy == "HireDate")">Hire Date</option>
                                        <option value="Salary" selected="@(ViewBag.SortBy == "Salary")">Salary</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <button type="submit" class="btn btn-primary">
                                    <i class="fas fa-search"></i> Search
                                </button>
                            </div>
                        </div>
                    </form>

                    <!-- Employees Table -->
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Email</th>
                                    <th>Department</th>
                                    <th>Hire Date</th>
                                    <th>Salary</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in Model.Items)
                                {
                                    <tr>
                                        <td>@item.FirstName @item.LastName</td>
                                        <td>@item.Email</td>
                                        <td>@item.Department?.Name</td>
                                        <td>@item.HireDate.ToShortDateString()</td>
                                        <td>@item.Salary.ToString("C")</td>
                                        <td>
                                            <div class="btn-group" role="group">
                                                <a asp-action="Details" asp-route-id="@item.Id" class="btn btn-info btn-sm">
                                                    <i class="fas fa-eye"></i> Details
                                                </a>
                                                <a asp-action="Edit" asp-route-id="@item.Id" class="btn btn-warning btn-sm">
                                                    <i class="fas fa-edit"></i> Edit
                                                </a>
                                                <a asp-action="Delete" asp-route-id="@item.Id" class="btn btn-danger btn-sm">
                                                    <i class="fas fa-trash"></i> Delete
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    </div>

                    <!-- Pagination -->
                    @if (Model.TotalCount > 0)
                    {
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <p class="text-muted">
                                    Showing @(((Model.Page - 1) * Model.PageSize) + 1) to @Math.Min(Model.Page * Model.PageSize, Model.TotalCount) of @Model.TotalCount entries
                                </p>
                            </div>
                            <nav>
                                <ul class="pagination">
                                    @{
                                        var totalPages = (int)Math.Ceiling((double)Model.TotalCount / Model.PageSize);
                                        var startPage = Math.Max(1, Model.Page - 2);
                                        var endPage = Math.Min(totalPages, Model.Page + 2);
                                    }

                                    @if (Model.Page > 1)
                                    {
                                        <li class="page-item">
                                            <a class="page-link" href="@Url.Action("Index", new { page = Model.Page - 1, pageSize = Model.PageSize, search = ViewBag.Search, sortBy = ViewBag.SortBy, sortDir = ViewBag.SortDir, departmentId = ViewBag.DepartmentId })">
                                                <i class="fas fa-chevron-left"></i>
                                            </a>
                                        </li>
                                    }

                                    @for (int i = startPage; i <= endPage; i++)
                                    {
                                        <li class="page-item @(i == Model.Page ? "active" : "")">
                                            <a class="page-link" href="@Url.Action("Index", new { page = i, pageSize = Model.PageSize, search = ViewBag.Search, sortBy = ViewBag.SortBy, sortDir = ViewBag.SortDir, departmentId = ViewBag.DepartmentId })">@i</a>
                                        </li>
                                    }

                                    @if (Model.Page < totalPages)
                                    {
                                        <li class="page-item">
                                            <a class="page-link" href="@Url.Action("Index", new { page = Model.Page + 1, pageSize = Model.PageSize, search = ViewBag.Search, sortBy = ViewBag.SortBy, sortDir = ViewBag.SortDir, departmentId = ViewBag.DepartmentId })">
                                                <i class="fas fa-chevron-right"></i>
                                            </a>
                                        </li>
                                    }
                                </ul>
                            </nav>
                        </div>
                    }
                    else
                    {
                        <div class="text-center py-4">
                            <i class="fas fa-users fa-3x text-muted mb-3"></i>
                            <h4 class="text-muted">No employees found</h4>
                            <p class="text-muted">Try adjusting your search criteria or create a new employee.</p>
                        </div>
                    }
                </div>
            </div>
        </div>
    </div>
</div>
```

**EMS.Web/Views/Employee/Create.cshtml:**
```html
@model EMS.Domain.Models.Employee

@{
    ViewData["Title"] = "Create Employee";
}

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Create Employee</h3>
                    <div class="card-tools">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="fas fa-arrow-left"></i> Back to List
                        </a>
                    </div>
                </div>
                <div class="card-body">
                    <form asp-action="Create">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="FirstName" class="control-label"></label>
                                    <input asp-for="FirstName" class="form-control" placeholder="Enter first name" />
                                    <span asp-validation-for="FirstName" class="text-danger"></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="LastName" class="control-label"></label>
                                    <input asp-for="LastName" class="form-control" placeholder="Enter last name" />
                                    <span asp-validation-for="LastName" class="text-danger"></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label asp-for="Email" class="control-label"></label>
                            <input asp-for="Email" class="form-control" placeholder="Enter email address" type="email" />
                            <span asp-validation-for="Email" class="text-danger"></span>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="HireDate" class="control-label"></label>
                                    <input asp-for="HireDate" class="form-control" type="date" />
                                    <span asp-validation-for="HireDate" class="text-danger"></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="Salary" class="control-label"></label>
                                    <input asp-for="Salary" class="form-control" placeholder="Enter salary" type="number" step="0.01" min="0.01" />
                                    <span asp-validation-for="Salary" class="text-danger"></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label asp-for="DepartmentId" class="control-label">Department</label>
                            <select asp-for="DepartmentId" class="form-control" asp-items="ViewBag.Departments">
                                <option value="">-- Select Department --</option>
                            </select>
                            <span asp-validation-for="DepartmentId" class="text-danger"></span>
                        </div>
                        
                        <div class="form-group">
                            <button type="submit" class="btn btn-primary">
                                <i class="fas fa-save"></i> Create
                            </button>
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="fas fa-times"></i> Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
```

**EMS.Web/Views/Employee/Edit.cshtml:**
```html
@model EMS.Domain.Models.Employee

@{
    ViewData["Title"] = "Edit Employee";
}

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Edit Employee</h3>
                    <div class="card-tools">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="fas fa-arrow-left"></i> Back to List
                        </a>
                    </div>
                </div>
                <div class="card-body">
                    <form asp-action="Edit">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <input type="hidden" asp-for="Id" />
                        
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="FirstName" class="control-label"></label>
                                    <input asp-for="FirstName" class="form-control" placeholder="Enter first name" />
                                    <span asp-validation-for="FirstName" class="text-danger"></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="LastName" class="control-label"></label>
                                    <input asp-for="LastName" class="form-control" placeholder="Enter last name" />
                                    <span asp-validation-for="LastName" class="text-danger"></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label asp-for="Email" class="control-label"></label>
                            <input asp-for="Email" class="form-control" placeholder="Enter email address" type="email" />
                            <span asp-validation-for="Email" class="text-danger"></span>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="HireDate" class="control-label"></label>
                                    <input asp-for="HireDate" class="form-control" type="date" />
                                    <span asp-validation-for="HireDate" class="text-danger"></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label asp-for="Salary" class="control-label"></label>
                                    <input asp-for="Salary" class="form-control" placeholder="Enter salary" type="number" step="0.01" min="0.01" />
                                    <span asp-validation-for="Salary" class="text-danger"></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label asp-for="DepartmentId" class="control-label">Department</label>
                            <select asp-for="DepartmentId" class="form-control" asp-items="ViewBag.Departments">
                                <option value="">-- Select Department --</option>
                            </select>
                            <span asp-validation-for="DepartmentId" class="text-danger"></span>
                        </div>
                        
                        <div class="form-group">
                            <button type="submit" class="btn btn-primary">
                                <i class="fas fa-save"></i> Save
                            </button>
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="fas fa-times"></i> Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
```

**EMS.Web/Views/Employee/Details.cshtml:**
```html
@model EMS.Domain.Models.Employee

@{
    ViewData["Title"] = "Employee Details";
}

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Employee Details</h3>
                    <div class="card-tools">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="fas fa-arrow-left"></i> Back to List
                        </a>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <dl class="row">
                                <dt class="col-sm-4">Name</dt>
                                <dd class="col-sm-8">@Model.FirstName @Model.LastName</dd>

                                <dt class="col-sm-4">Email</dt>
                                <dd class="col-sm-8">@Model.Email</dd>

                                <dt class="col-sm-4">Department</dt>
                                <dd class="col-sm-8">@Model.Department?.Name</dd>
                            </dl>
                        </div>
                        <div class="col-md-6">
                            <dl class="row">
                                <dt class="col-sm-4">Hire Date</dt>
                                <dd class="col-sm-8">@Model.HireDate.ToShortDateString()</dd>

                                <dt class="col-sm-4">Salary</dt>
                                <dd class="col-sm-8">@Model.Salary.ToString("C")</dd>

                                <dt class="col-sm-4">Employee ID</dt>
                                <dd class="col-sm-8">@Model.Id</dd>
                            </dl>
                        </div>
                    </div>

                    <div class="mt-4">
                        <div class="btn-group" role="group">
                            <a asp-action="Edit" asp-route-id="@Model.Id" class="btn btn-warning">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            <a asp-action="Delete" asp-route-id="@Model.Id" class="btn btn-danger">
                                <i class="fas fa-trash"></i> Delete
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
```

**EMS.Web/Views/Employee/Delete.cshtml:**
```html
@model EMS.Domain.Models.Employee

@{
    ViewData["Title"] = "Delete Employee";
}

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Delete Employee</h3>
                    <div class="card-tools">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="fas fa-arrow-left"></i> Back to List
                        </a>
                    </div>
                </div>
                <div class="card-body">
                    <div class="alert alert-danger">
                        <h4><i class="fas fa-exclamation-triangle"></i> Are you sure you want to delete this employee?</h4>
                        <p>This action cannot be undone.</p>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <dl class="row">
                                <dt class="col-sm-4">Name</dt>
                                <dd class="col-sm-8">@Model.FirstName @Model.LastName</dd>

                                <dt class="col-sm-4">Email</dt>
                                <dd class="col-sm-8">@Model.Email</dd>

                                <dt class="col-sm-4">Department</dt>
                                <dd class="col-sm-8">@Model.Department?.Name</dd>
                            </dl>
                        </div>
                        <div class="col-md-6">
                            <dl class="row">
                                <dt class="col-sm-4">Hire Date</dt>
                                <dd class="col-sm-8">@Model.HireDate.ToShortDateString()</dd>

                                <dt class="col-sm-4">Salary</dt>
                                <dd class="col-sm-8">@Model.Salary.ToString("C")</dd>

                                <dt class="col-sm-4">Employee ID</dt>
                                <dd class="col-sm-8">@Model.Id</dd>
                            </dl>
                        </div>
                    </div>

                    <form asp-action="Delete" class="mt-4">
                        <input type="hidden" asp-for="Id" />
                        <button type="submit" class="btn btn-danger">
                            <i class="fas fa-trash"></i> Delete
                        </button>
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="fas fa-times"></i> Cancel
                        </a>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
```

## 🔧 **Fix 2: HTTPS Configuration**

Now let's fix the HTTPS issue by updating the launch settings:

**EMS.Web/Properties/launchSettings.json:**
```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5234",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7281;http://localhost:5234",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## 🚀 **Steps to fix:**

1. **Create the Employee folder and views:**
   ```bash
   mkdir EMS.Web\Views\Employee
   ```

2. **Copy the view files I provided above into the new folder**

3. **Update the launch settings file**

4. **Run the application:**
   ```bash
   cd EMS.Web
   dotnet run --launch-profile https
   ```

## 🎯 **Expected URLs after fix:**

- **HTTPS**: `https://localhost:7281` (should work now)
- **HTTP**: `http://localhost:5234` (fallback)

The Employee views should now work properly, and HTTPS should be configured correctly!
