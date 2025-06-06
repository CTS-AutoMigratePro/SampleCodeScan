using Microsoft.Data.SqlClient;
using SampleCompany.API.Models;

namespace SampleCompany.API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionstring;

        public DepartmentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionstring = configuration.GetConnectionString("SqlConnection");
        }
        public async Task CreateDepartment(Department department)
        {
            using var conn = new SqlConnection(connectionstring);
            using var cmd = new SqlCommand("dbo.spInsertDepartment", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
