using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCompany.API.Models;
using SampleCompany.API.Repositories;

namespace SampleCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null");
            }
            await repository.CreateEmployee(employee);
            return Ok("Successfully Added!");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await repository.GetAllEmployee();
            if (employees == null || !employees.Any())
            {
                return NotFound("No Employees Found");
            }
            return Ok(employees);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null");
            }
            await repository.UpdateEmployee(employee);
            return Ok("Successfully Updated!");
        }
    }
}
