using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class ExercisePlan
    {
        public ExercisePlan()
        {
        }

        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
