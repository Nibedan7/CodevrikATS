using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdplATS.Entity.Models
{
    public class Enum
    {
        #region Leave/ManualLogStatusEnum
            public enum statusEnum
            {
                Pending = 1,
                Deleted = 2,
                Approved = 3,
                Rejected = 4,
                Cancelled = 5,
            }
        #endregion

        #region WeekdayEnum

        public enum WeekDay
        {
            Sunday = 1,
            Monday = 2,
            Tuesday = 3,
            Wednesday = 4,
            Thursday = 5,
            Friday = 6,
            Saturday = 7
        }

        #endregion

        #region LeaveTypeEnum
        public enum LeaveType
        {
            [Display(Name = "Full Day")]
            FullDay = 1,
            [Display(Name = "First Half")]
            FirstHalf = 2,
            [Display(Name = "Second Half")]
            SecondHalf = 3,
        }
        #endregion
    }
}
