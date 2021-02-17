using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class ExercisePlanSchedule
    {
        public int Id { get; set; }
        public int ExercisePlanId { get; set; }
        public int DayOfWeek { get; set; }
        public int HourOfDay { get; set; }

        public virtual ExercisePlan ExercisePlan { get; set; }
    }
}
