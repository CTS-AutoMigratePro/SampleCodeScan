using SampleCompany.API.DTOs;
using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<AllEmployeeDTO>> GetAllEmployee();
        Task CreateEmployee(Employee employee);
        Task DeleteEmployee(int EmployeeID);
        Task UpdateEmployee(Employee employee);
    }
}
