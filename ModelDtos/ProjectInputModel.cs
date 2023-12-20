namespace WebApiAuth.ModelDtos
{
    public class ProjectInputModel
    {
        public string ProjectName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public List<int> EmployeeIds { get; set; }
    }
}
