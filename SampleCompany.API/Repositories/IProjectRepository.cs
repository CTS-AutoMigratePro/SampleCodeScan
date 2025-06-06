using SampleCompany.API.DTOs;
using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public interface IProjectRepository
    {
        Task CreateProject(Project project);
        Task AssignProject(EmployeeProject employeeProject);
        Task<IEnumerable<GetEmployeeWithProject>> GetEmployeeWithProject();
        Task CreateEmployyeeAndProject(CreateEmpAndAssignProject create);
    }
}
