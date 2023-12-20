using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newProj.Models;
using WebApiAuth.Data;
using WebApiAuth.ModelDtos;
using WebApiAuth.Services.ProjectServices;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult GetProjects(int page = 1, int pageSize = 10)
    {
        var projects = _context.projects
            .Include(p => p.ProjectManager)
            .Include(p => p.Employees)
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = projects.Select(p => new
        {
            ProjectName = p.Name,
            ProjectStartDate = p.StartDate,
            ProjectEndDate = p.EndDate,
            ProjectManagerName = p.ProjectManager != null ? p.ProjectManager.Name : null,
            ProjectManagerEmail = p.ProjectManager != null ? p.ProjectManager.Email : null,
            Employees = p.Employees.Select(e => new
            {
                Name = e.Name,
                Email = e.Email,
                DOJ = e.DOJ,
                DepartmentName = _context.departments.FirstOrDefault(d => d.Id == e.DepartmentId)?.Name
            }).ToList()
        }).ToList();

        return new OkObjectResult(new
        {
            data = result,
            page,
            pageSize,
            hasMore = projects.Count > page * pageSize,
            totalItems = projects.Count
        });
    }

    public IActionResult AddProject(ProjectInputModel projectInput)
    {
       // var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();


        var projectManager = _context.employees.Find(projectInput.ProjectManagerId);
        if (projectManager == null)
        {
            return new BadRequestObjectResult("Invalid Project Manager ID");
        }

        var existingEmployees = _context.employees.Where(e => projectInput.EmployeeIds.Contains(e.Id)).ToList();

        var newProject = new Project
        {
            Name = projectInput.ProjectName,
            StartDate = projectInput.ProjectStartDate,
            EndDate = projectInput.ProjectEndDate,
            ProjectManagerId = projectInput.ProjectManagerId,
            Employees = existingEmployees
        };

        _context.projects.Add(newProject);
        _context.SaveChanges();

        return new OkObjectResult("Project added successfully");
    }

    public IActionResult AddDepartment(DepartmentInputModel departmentInput)
    {
      

        var newDepartment = new Department
        {
            Name = departmentInput.DepartmentName
        };

        _context.departments.Add(newDepartment);
        _context.SaveChanges();

        return new OkObjectResult("Department added successfully");
    }

    public IActionResult AddEmployee(EmployeeInputModel employeeInput)
    {
       

        var department = _context.departments.Find(employeeInput.DepartmentId);
        if (department == null)
        {
            return new BadRequestObjectResult("Invalid Department ID");
        }

        var newEmployee = new Employee
        {
            Name = employeeInput.EmployeeName,
            Email = employeeInput.EmployeeEmail,
            DOJ = employeeInput.EmployeeDOJ,
            DepartmentId = employeeInput.DepartmentId
        };

        _context.employees.Add(newEmployee);
        _context.SaveChanges();

        return new OkObjectResult("Employee added successfully");
    }
}
