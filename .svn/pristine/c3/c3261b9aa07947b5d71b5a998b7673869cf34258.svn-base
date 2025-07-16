using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static CdplATS.Entity.Models.Enum;
namespace CdplATS.Entity.Models
{
    public class HoliDayEntity
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Festival name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Festival name must be between 3 and 100 characters.")]
        public string FestivalName { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime? Date { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeekDay? WeekDay { get; set; }

        public List<int>? HolidayYears { get; set; }
        public int? CurrentYear { get; set; }



    }
}
