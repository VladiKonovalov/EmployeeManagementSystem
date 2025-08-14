using EMS.Infrastructure.Data;
using EMS.Infrastructure.Extensions;
using EMS.Web.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        new Serilog.Formatting.Compact.CompactJsonFormatter(),
        "Logs/log-.json",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

// Ensure required directories exist
var appDataPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
var logsPath = Path.Combine(builder.Environment.ContentRootPath, "Logs");
if (!Directory.Exists(appDataPath)) { Directory.CreateDirectory(appDataPath); Log.Information("Created App_Data directory"); }
if (!Directory.Exists(logsPath)) { Directory.CreateDirectory(logsPath); Log.Information("Created Logs directory"); }

builder.Services.AddControllersWithViews();

// Add Infrastructure services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString)) { throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); }
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

// Apply database migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try 
    { 
        context.Database.Migrate(); 
        Log.Information("Database migrations applied successfully");
        
        // Seed departments if none exist
        if (!context.Departments.Any())
        {
            context.Departments.AddRange(
                new EMS.Domain.Models.Department { Name = "IT" },
                new EMS.Domain.Models.Department { Name = "HR" },
                new EMS.Domain.Models.Department { Name = "Finance" },
                new EMS.Domain.Models.Department { Name = "Marketing" }
            );
            context.SaveChanges();
            Log.Information("Seed data added successfully");
        }
    }
    catch (Exception ex) 
    { 
        Log.Error(ex, "An error occurred while applying database migrations"); 
        throw; 
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Global exception handling middleware
app.UseGlobalExceptionHandling();

// Only use HTTPS redirection in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

try { Log.Information("Starting EMS Web application"); app.Run(); }
catch (Exception ex) { Log.Fatal(ex, "Application terminated unexpectedly"); }
finally { Log.CloseAndFlush(); }
