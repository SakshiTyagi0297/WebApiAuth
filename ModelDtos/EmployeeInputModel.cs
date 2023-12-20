namespace WebApiAuth.ModelDtos
{
    public class EmployeeInputModel
    {
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public DateTime EmployeeDOJ { get; set; }
        public int DepartmentId { get; set; }
    }
}
