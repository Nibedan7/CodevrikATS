using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdplATS.Entity.Models
{
    public class AppraisalEntity
    {
        public int EmployeeCode { get; set; }
        public int LoggedEmpCode { get; set; }
        public int EmployeeAssessmentId { get; set; }
        public int? TabId { get; set; }
        public int AssessmentCategoryId { get; set; }
        public string? AssessmentCategoryName { get; set; }
        public int AssessmentId { get; set; }
        public string? Assessment { get; set; }
        public string? PlaceHolder { get; set; }
        public int? SelfRating { get; set; }
        public int? CorrectedSelfRating { get; set; }
        public string? SelfComment { get; set; }
        public int? IsSelfAssSubmitted { get; set; }
        public int? Reviewer1_Rating { get; set; }
        public string? Reviewer1_Comment { get; set; }
        public int? IsReviewer1_Submitted { get; set; }
        public int? Reviewer2_Rating { get; set; }
        public string? Reviewer2_Comment { get; set; }
        public int? IsReviewer2_Submitted { get; set; }
        public int? IsRating { get; set; }
        public string? Reviewer1Name { get; set; }
        public string? Reviewer2Name { get; set; }
        public int? Reviewer1Id { get; set; }
        public int? Reviewer2Id { get; set; }
        public List<BindApprisalEmployeesList>? AppraisalEmployeeList { get; set; }
        public int? DefaultAppraisalEmployee { get; set; }
        public int? IsSubmitted { get; set; }
        public string? JsonString { get; set; }
        public decimal SelfRatingAVG { get; set; }
        public decimal Reviewer1RatingAVG { get; set; }
        public decimal Reviewer2RatingAVG { get; set; }
    }

    public class BindApprisalByEmployeeList
    {
        public int EmployeeAssessmentId { get; set; }
        public int EmployeeCode { get; set; }
        public string AssessmentName { get; set; }

    }
    
    public class BindApprisalEmployeesList
    {
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
    }

    public class AvarageAppraisal
    {
        public decimal SelfRatingAVG { get; set; }
        public decimal Reviewer1RatingAVG { get; set; }
        public decimal Reviewer2RatingAVG { get; set; }
        public int Reviewer1Id { get; set; }
        public int Reviewer2Id { get; set; }
    }


    public class UpdateAssessment
    {
        public int AssessmentId { get; set; }
        public int AssessmentCategoryId { get; set; }

        public int? SelfRating { get; set; }
        public string SelfComment { get; set; }

        public int? Reviewer1_Rating { get; set; }
        public string Reviewer1_Comment { get; set; }

        public int? Reviewer2_Rating { get; set; }
        public string Reviewer2_Comment { get; set; }
    }
}
