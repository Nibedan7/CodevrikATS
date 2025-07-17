namespace CodeVrikATS.Entity.Models
{
    public class LeaveReportEntity
    {
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int TotalLeave { get; set; }
        public float? UsedLeave { get; set; }
        public string? LeaveDates { get; set; }
        public List<int>? Years { get; set; }
    }
}
