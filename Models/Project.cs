using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace newProj.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ProjectManagerId { get; set; }

        [ForeignKey("ProjectManagerId")]
        public Employee ProjectManager { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
