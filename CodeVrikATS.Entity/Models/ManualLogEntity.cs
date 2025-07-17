using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeVrikATS.Entity.Models.Enum;

namespace CodeVrikATS.Entity.Models
{
    public class ManualLogEntity
    {
        public int? Id { get; set; }
        public int? EmpCode { get; set; }
        public string? EmployeeName { get; set; }
        public int PunchType { get; set; }
        public DateTime? PunchTime { get; set; }
        public string? Reason { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public statusEnum? Status { get; set; }

        public int? LogEmpCode { get; set; }

        public List<EmpDropDownList>? EmployeeDropDownList { get; set; }
    }
}
