using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class ExercisePlan
    {
        public ExercisePlan()
        {
            ExercisePlanSchedules = new HashSet<ExercisePlanSchedule>();
        }

        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public int EndDate { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<ExercisePlanSchedule> ExercisePlanSchedules { get; set; }
    }
}
