using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CdplATS.Entity.Models
{
    public class DepartmentEntity
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Choose leader")]
        public int LeadBy { get; set; }
        public string? LeadByName { get; set; }
        public int? LoggedEmployeeCode { get; set; }
        public List<EmployeeEntity>? EmployeeList { get; set; }
        public string? SelectEmployees { get; set; } 
        public string? EmployeeNames { get; set; }
        public List<int>? SelectedEmployeeIds { get; set; }
        public int? EmployeeCode { get; set; }
        public List<string>? SelectedEmployeeCodes { get; set; } = new List<string>();

    }
}
