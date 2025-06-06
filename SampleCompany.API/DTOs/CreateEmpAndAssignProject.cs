namespace SampleCompany.API.DTOs
{
    public class CreateEmpAndAssignProject
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentID { get; set; }
        public int ProjectID { get; set; }
    }
}
