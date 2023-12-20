using Microsoft.AspNetCore.Mvc;
using WebApiAuth.ModelDtos;

namespace WebApiAuth.Services.ProjectServices
{
    public interface IProjectService
    {
        IActionResult GetProjects(int page = 1, int pageSize = 10);
        IActionResult AddProject(ProjectInputModel projectInput);
        IActionResult AddDepartment(DepartmentInputModel departmentInput);
        IActionResult AddEmployee(EmployeeInputModel employeeInput);
    }
}
