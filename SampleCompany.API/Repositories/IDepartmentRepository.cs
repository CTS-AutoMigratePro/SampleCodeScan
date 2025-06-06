using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public interface IDepartmentRepository
    {
        Task CreateDepartment(Department department);
    }
}
