using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCompany.API.Models;
using SampleCompany.API.Repositories;

namespace SampleCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department cannot be null");
            }
            await repository.CreateDepartment(department);
            return Ok("Successfully Added!");
        }

    }
}
