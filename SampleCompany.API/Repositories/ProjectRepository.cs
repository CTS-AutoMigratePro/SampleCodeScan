
using Microsoft.Data.SqlClient;
using SampleCompany.API.DTOs;
using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public ProjectRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task AssignProject(EmployeeProject employeeProject)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spAssignProject", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectId", employeeProject.ProjectId);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeProject.EmployeeId);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

        }

        public async Task CreateEmployyeeAndProject(CreateEmpAndAssignProject create)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spCreateEmpAndAssignProject", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", create.Name);
            cmd.Parameters.AddWithValue("@Email", create.Email);
            cmd.Parameters.AddWithValue("@DepId", create.DepartmentID);
            cmd.Parameters.AddWithValue("@ProjectID", create.ProjectID);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task CreateProject(Project project)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spInsertProject", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<GetEmployeeWithProject>> GetEmployeeWithProject()
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spGetEmployeeWithProject", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var EmpWithProj = new List<GetEmployeeWithProject>();
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while(reader.Read())
            {
                EmpWithProj.Add(new GetEmployeeWithProject
                {
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                    ProjectName = reader["ProjectName"].ToString()
                });
            }
            return EmpWithProj;
        }
    }
}
