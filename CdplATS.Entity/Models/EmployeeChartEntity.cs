namespace CodeVrikATS.Entity.Models
{
    public class EmployeeChartEntity
    {
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int? Month { get; set; }
        public string? MonthName { get; set; }
        public int? Year { get; set; }
        public decimal? ExpectedHours { get; set; }
        public decimal? WorkedHours { get; set; }
        public List<EmpDropDownList>? EmployeeHierarchyList { get; set; }
        public int? SelectedEmployeeCode { get; set; }  

        public string Date { get; set; }
        public List<int> bindYear { get; set; }

    }



    public class AverageWorkHourEntity
    {
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public decimal TotalHoursWorked { get; set; }
        public decimal TotalWorkingDays { get; set; }
        public decimal WeightedWorkingDays { get; set; }
        public decimal AverageWorkHours { get; set; }
    }

    public class PunctualityRateEntity
    {
        public string EmployeeCode { get; set; }
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        public int TotalWorkingDays { get; set; }
        public decimal EarlyPercent { get; set; }
        public decimal OnTimePercent { get; set; }
        public decimal LatePercent { get; set; }
    }

    public class GetWFHvsOfficeEntity
    {
        public string EmployeeCode { get; set; }
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        //public int TotalWorkingDays { get; set; }
        public int TotalDays { get; set; }
        public int WFH_Days { get; set; }
        public int WFO_Days { get; set; }
        public decimal WFH_Percent { get; set; }
        public decimal WFO_Percent { get; set; }
        
       
    }

    public class GetMonthlyWorkHourEntity
    {
        public string EmployeeCode { get; set; }
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        public int TotalWorkingHours { get; set; }
        public decimal ActualWorkingHour { get; set; }
        public decimal WorkDone { get; set; }


    }
}