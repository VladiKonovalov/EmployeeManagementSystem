# Employee Management System (EMS)

A modern Employee Management System built with ASP.NET Core 8.0, featuring a clean layered architecture, comprehensive CRUD operations, and a responsive web interface.


small preview of the system : https://youtu.be/0ZGwOiE8FCU


screenshots from the system:

<img width="1835" height="870" alt="image" src="https://github.com/user-attachments/assets/0199f2f1-ed24-4ea5-956f-71857c347ca8" />
<img width="1907" height="735" alt="image" src="https://github.com/user-attachments/assets/ffa14305-8e29-482b-bca3-249a8c23fd51" />

<img width="1901" height="853" alt="image" src="https://github.com/user-attachments/assets/7af22f1a-1ed2-42c0-869f-773e92b07faa" />


*Modern dashboard with real-time statistics, employee management, and department overview*

## 🚀 Features

- **Employee Management**: Complete CRUD operations with validation
- **Department Management**: Department CRUD with employee relationship handling
- **Advanced Search & Filtering**: Search by name/email, filter by department, sort by multiple fields
- **Pagination**: Efficient data display with configurable page sizes
- **Dashboard**: Real-time statistics and overview
- **Responsive Design**: Modern UI built with Bootstrap and FontAwesome
- **Docker Support**: Containerized deployment ready

## 🏗️ Tech Stack

- **.NET 8.0** - Latest LTS version
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core** - ORM with SQLite
- **Serilog** - Structured logging
- **Bootstrap 5** - UI framework
- **FontAwesome** - Icons
- **Docker** - Containerization

## 📋 Prerequisites

- **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Docker** (optional) - [Download here](https://www.docker.com/products/docker-desktop/)

## 🛠️ Quick Start

### **Local Development**

1. **Clone and navigate to the project**
   ```bash
   git clone <repository-url>
   cd EmployeeManagementSystem
   ```

2. **Run the application**
   ```bash
   cd EMS.Web
   dotnet run
   ```

3. **Access the application**
   - **Primary URL**: `http://localhost:5000`
   - **Alternative URL**: `http://localhost:5234`
   - The application will automatically create the database and apply migrations on first run

### **Docker Deployment**

1. **Build and run with Docker**
   ```bash
   docker build -t ems-app .
   docker run -p 8080:8080 ems-app
   ```

2. **Access the application**
   - Open your browser and navigate to `http://localhost:8080`

## 🗄️ Database

- **Location**: `EMS.Web/App_Data/ems.db`
- **Type**: SQLite (file-based)
- **Migrations**: Applied automatically on startup
- **Schema**: Departments and Employees with one-to-many relationship

## 📊 Application Structure

```
EmployeeManagementSystem/
├── EMS.Domain/           # Domain Layer (Entities, Validation)
├── EMS.Application/      # Application Layer (Interfaces, DTOs)
├── EMS.Infrastructure/   # Infrastructure Layer (Data, Services)
└── EMS.Web/             # Presentation Layer (Controllers, Views)
```

## 🧪 Testing

1. **Create Departments**: Navigate to Departments → Create New
2. **Add Employees**: Navigate to Employees → Create New
3. **Test Features**: Search, filter, sort, pagination

## 📝 License

This project is licensed under the MIT License.

---

**Built with ❤️ using ASP.NET Core 8.0**
