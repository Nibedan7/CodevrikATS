namespace CodeVrikATS.Entity.Models
{
    public class DashboardEntity
    {
        public List<int> Year { get; set; }

        public int CurrentYear { get; set; }
    }
    public class GetTotalWorkHourPerEmployeeEntity
    {
        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int TotalWorkingHours { get; set; }
        public int ActualWorkingHour { get; set; }
        public decimal WorkDone { get; set; }

    }

    public class GetMonthlyAvgWorkHoursPerDayEntity
    {
        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int TotalHours { get; set; }
        public decimal AverageHoursPerDay { get; set; }

    }

    public class GetEmployeePunctualityEntity
    {
        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int TotalWorkingDays { get; set; }
        public int OnTimeDays { get; set; }
        public decimal OnTimePercent { get; set; }

    }

    public class GetAnnualLeaveSummaryByYearEntity
    {
        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int TotalAnnualLeave { get; set; }
        public decimal UsedLeave { get; set; }
        public decimal OverLeave { get; set; }

    }

    public class MonitorHybridWorkBalanceEntity
    {
        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int TotalDays { get; set; }
        public int wfh_days { get; set; }
        public decimal wfo_days { get; set; }
        public decimal wfh_percent { get; set; }
        public decimal wfo_percent { get; set; }


    }

}
