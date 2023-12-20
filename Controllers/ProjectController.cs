// ProjectController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAuth.Data;
using WebApiAuth.ModelDtos;
using WebApiAuth.Services.ProjectServices;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult GetProjects(int page = 1, int pageSize = 10)
    {
        return _projectService.GetProjects(page, pageSize);
    }

    [HttpPost("addProject")]
    public IActionResult AddProject(ProjectInputModel projectInput)
    {
        return _projectService.AddProject(projectInput);
    }

    [HttpPost("AddDepartment")]
    public IActionResult AddDepartment(DepartmentInputModel departmentInput)
    {
        return _projectService.AddDepartment(departmentInput);
    }

    [HttpPost("AddEmployee")]
    public IActionResult AddEmployee(EmployeeInputModel employeeInput)
    {
        return _projectService.AddEmployee(employeeInput);
    }
}
