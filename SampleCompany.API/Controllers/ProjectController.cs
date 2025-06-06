using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCompany.API.DTOs;
using SampleCompany.API.Models;
using SampleCompany.API.Repositories;

namespace SampleCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository repository;

        public ProjectController(IProjectRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest("Project cannot be null");
            }
            await repository.CreateProject(project);
            return Ok("Project created successfully");
        }
        [HttpPost]
        [Route("AssignProject")]
        public async Task<IActionResult> AssignProject([FromBody] EmployeeProject employeeProject)
        {
            if (employeeProject == null)
            {
                return BadRequest("EmployeeProject cannot be null");
            }
            await repository.AssignProject(employeeProject);
            return Ok("Project assigned successfully");
        }
        [HttpGet]
        [Route("GetEmployeeWithProject")]
        public async Task<IActionResult> GetEmployeeWithProject()
        {
            var result = await repository.GetEmployeeWithProject();
            if (result == null || !result.Any())
            {
                return NotFound("No employees found with projects");
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateEmployeeAndProject")]
        public async Task<IActionResult> CreateEmployeeAndProject([FromBody] CreateEmpAndAssignProject create)
        {
            if (create == null)
            {
                return BadRequest("CreateEmpAndAssignProject cannot be null");
            }
            await repository.CreateEmployyeeAndProject(create);
            return Ok("Employee and project created and assigned successfully");
        }
    }
}
