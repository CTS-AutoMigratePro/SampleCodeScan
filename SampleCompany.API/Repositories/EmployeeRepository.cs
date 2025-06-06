using Microsoft.Data.SqlClient;
using SampleCompany.API.DTOs;
using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }
        public async Task CreateEmployee(Employee employee)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("spInsertEmployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name",employee.Name);
            cmd.Parameters.AddWithValue("@Email",employee.Email);
            cmd.Parameters.AddWithValue("@DepId", employee.DepartmentID);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteEmployee(int EmployeeID)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("spDeleteEmployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<AllEmployeeDTO>> GetAllEmployee()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("spGetAllEmployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var employees = new List<AllEmployeeDTO>();
            while (await reader.ReadAsync())
            {
                employees.Add(new AllEmployeeDTO
                {
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    DepartmentName = reader["DepartmentName"].ToString()
                });
            }
            return employees;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("spUpdateEmployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@DepId", employee.DepartmentID);
            conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

        }
    }
}
